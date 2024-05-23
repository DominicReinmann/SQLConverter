# SQLConverter

Simple CLI Tool to convert your SQL keywords to Uppercase 

## Installation
```bash
dotnet tool install --global .\nupkg\ SQLConverter
```

## Usage
```bash
# To convert a single file
sql <filename>

# To convert all files in a directory
sql -p <path> -a 

# To get help
sql -h
```
|Command|description|
|--|--|
|-h / --h| Opens Help|
|-p / --p | prefix for the path input|
|-a / --a | converts all sql files in the active directory|

## Update 
```bash
dotnet tool update --global SQLConverter --add-source .\nupkg\
```
