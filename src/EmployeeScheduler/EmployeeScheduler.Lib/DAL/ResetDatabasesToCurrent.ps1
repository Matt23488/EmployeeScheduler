Remove-Item –path .\EmployeeScheduler.Api/test.db
Remove-Item –path .\EmployeeScheduler.Test/test.db
Remove-Item -path .\EmployeeScheduler.Lib\Migrations\*

dotnet ef migrations add Initial
dotnet ef database update --project .\EmployeeScheduler.Lib --startup-project .\EmployeeScheduler.Api
dotnet ef database update --project .\EmployeeScheduler.Lib --startup-project .\EmployeeScheduler.Test
#This doesn't work. Fix it because that would be cool.