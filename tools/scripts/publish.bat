@REM Usage: publish.bat remoteName websiteUrl
rmdir /s /q ..\..\bin\publish
@REM Publish the application to the specified folder
npx tailwindcss -i tools/tailwindcss/input.css -o wwwroot/css/tailwindcss.css
dotnet publish ..\..\c18-98-m-csharp.csproj -c Release -o ..\..\bin\publish

@REM Copy to server
rclone copy ..\..\bin\publish\ %1:%2 -P

@REM configure server
rclone copy ..\deploy %1:%2/conf -P
ssh %1 "chmod +x ./%2/conf/publish.sh"
ssh %1 "sudo ./%2/conf/publish.sh %2 $HOME"
