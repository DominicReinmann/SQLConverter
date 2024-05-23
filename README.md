# SQLConverter

A simple CLI Tool that lets you convert all Sql keywords to uppercase so you dont have to toggle tab all the time while Coding

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
