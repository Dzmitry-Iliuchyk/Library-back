Для запуска приложения необходимо зайти в проект webApi в файл appsettings.json в секции ConnectionStrings настроить строку подключения к базе данных.

Необходимо открыть терминал и перейти в папку с решением (Library.sln)

Далее необходимо выполнить следующие комманды. 

`dotnet restore`

`dotnet tool install --global dotnet-ef`

`dotnet ef database update --project Library.Persistance\Library.DataAccess.csproj --startup-project Library.WebAPI\Library.WebAPI.csproj`

`dotnet build`

`dotnet run --project Library.WebAPI\Library.WebAPI.csproj`
