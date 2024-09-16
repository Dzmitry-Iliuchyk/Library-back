namespace Library.DataAccess.Exceptions {
    public class GroupNotFountException: DataAccessException {
        public override string Message => "Такой группы не существует!";
    }

}
