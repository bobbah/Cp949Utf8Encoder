# CP949 to UTF-8 Encoder

This simple command-line utility can be used to re-encode CP949-encoded files into UTF-8. Requires .NET 6 runtime or greater.

*Note that this utility assumes the encoding is CP949, it doesn't check in any way, your mileage may vary if you try to encode files in other encodings...*

Example usage:
```sh
# Cp949Utf8Encoder.exe [input directory] [pattern] [output]
Cp949Utf8Encoder.exe ./FilesToConvert/ *.cpp ./output
```