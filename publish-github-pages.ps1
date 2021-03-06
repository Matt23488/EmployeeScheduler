$pathToSolution = "./src/EmployeeScheduler/"
$projectName = "EmployeeScheduler.Web"
$repoName = "EmployeeScheduler"

Write-Output "----==== Publish $pathToSolution"
dotnet publish $pathToSolution -c Release -o ./dist/
Write-Output ""

Write-Output "----==== Copy from ./dist/$projectName/dist"
Copy-Item -Path "./dist/$projectName/dist/*" -Destination "./" -Recurse -Force
Write-Output ""

$indexFile = "./index.html"
$originalBaseUrlText = "<base href=""/"" />";
$targetBaseUrlText = "<base href=""/$repoName/"" />";

Write-Output "----==== Replace base href in $indexFile to be /$repoName/"
((Get-Content -path $indexFile -Raw) -replace $originalBaseUrlText, $targetBaseUrlText) | Set-Content -NoNewline -Path $indexFile
Write-Output ""

Write-Output "----==== Delete dist folder"
Remove-Item ./dist/ -Recurse