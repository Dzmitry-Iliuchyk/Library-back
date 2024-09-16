
using FluentValidation;
using Library.DataAccess.Exceptions;
using Library.Domain.Exceptions;
using Library.WebAPI.Contracts.Middleware;
using System.Net;
using ApplicationException = Library.Application.Exceptions.ApplicationException;

namespace Library.WebAPI.Middlewares {
    public class ExceptionMiddleware: IMiddleware {
        public async Task InvokeAsync( HttpContext context, RequestDelegate next ) {
            try {
                await next( context );
            }
            catch (BookTakenException ex) {
                await HandleDefaultException( context, ex, HttpStatusCode.BadRequest );
            }
            catch(DomainException ex) {
                await HandleDefaultException( context, ex, HttpStatusCode.BadRequest );
            }
            catch(DataAccessException ex) {
                await HandleDefaultException( context, ex, HttpStatusCode.BadRequest );
            }
            catch(ApplicationException ex) {
                await HandleDefaultException( context, ex, HttpStatusCode.BadRequest );
            }
            catch(ValidationException ex) {
                await HandleValidationException( context, ex );
            }
            catch (Exception ex) {
                await HandleDefaultException( context, ex, HttpStatusCode.InternalServerError );
            }
        }

        private static async Task HandleDefaultException( HttpContext context, Exception ex, HttpStatusCode ErrorCode ) {
            context.Response.StatusCode = (int)ErrorCode;

            var errorResponse = new ErrorResponse( ex.GetType().Name,(int)ErrorCode, [ ex.Message ] );

            await context.Response.WriteAsJsonAsync( errorResponse );
        }
        private static async Task HandleValidationException( HttpContext context, ValidationException ex ) {
            int errorCode = (int)HttpStatusCode.BadRequest;
            context.Response.StatusCode = errorCode;
            var errors = new List<string>();
            foreach (var error in ex.Errors) {
                errors.Add( error.ErrorMessage);
            }
            var errorResponse = new ErrorResponse( ex.GetType().Name, errorCode, errors.ToArray() );

            await context.Response.WriteAsJsonAsync( errorResponse );
        }
    }
}
