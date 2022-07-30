rmdir /Q /S publish
dotnet publish Cp949Utf8Encoder -o publish/linux-x64/Cp949Utf8Encoder/ -r "linux-x64" --self-contained false -c Release
7z a publish/Cp949Utf8Encoder-linux-x64.zip -r ./publish/linux-x64/Cp949Utf8Encoder/*
dotnet publish Cp949Utf8Encoder -o publish/win-x64/Cp949Utf8Encoder/ -r "win-x64" --self-contained false -c Release
7z a publish/Cp949Utf8Encoder-win-x64.zip -r ./publish/win-x64/Cp949Utf8Encoder/*