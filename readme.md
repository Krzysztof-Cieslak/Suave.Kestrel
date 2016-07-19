# Suave.Kestrel

Using Suave on top of Kestrel Server.

**It's super early alpha version (let's call it RC) or proof of concept, not ready for any serious usage**

## Why?

Because Suave has great API for functional / F# programmers

Because Kestrel apparently is super fast.

## How to build project and run sample?

```
dotnet restore
dotnet build .\src\Suave.Kestrel\ .\samples\SimpleSample\
dotnet run -p .\samples\SimpleSample\
```

## Performance

Discussion about performance testing and some benchmarks results can be found at https://github.com/Krzysztof-Cieslak/Suave.Kestrel/issues/1

## How to build for benchmarks

### Suave on CoreCLR

```
dotnet restore
dotnet build ./benchmark/SuaveOnCoreCLR/
dotnet run -p ./benchmark/SuaveOnCoreCLR/
```
This should be running under 127.0.0.1:8081

### Suave on Mono

```
cd ./benchmark/SuaveOnMono
./build.sh 
mono ./build/SuaveOnMono.exe
```

This should be running under 127.0.0.1:8083

### Running wrk
Install wrk [Linux](https://github.com/wg/wrk/wiki/Installing-Wrk-on-Linux) | [OSX](https://github.com/wg/wrk/wiki/Installing-wrk-on-OSX)

Quick helper script to run against port 8080 to port 8084  

```
./benchmark.sh
```


## Known issues

Suave.LibUv on Mono still suffers from https://github.com/SuaveIO/suave/issues/360 and cannot get an accurate benchmark.

The Suave.LibUv on CoreCLR has trouble resolving packages and cannot be properly benchmarked

```
    Package Suave.LibUv 2.0.0-alpha4 is not compatible with netcoreapp1.0 (.NETCoreApp,Version=v1.0). Package Suave.LibUv 2.0.0-alpha4 supports: net40 (.NETFramework,Version=v4.0)
    Package FSharp.Core 3.1.2.5 is not compatible with netcoreapp1.0 (.NETCoreApp,Version=v1.0). Package FSharp.Core 3.1.2.5 supports:
      - net20 (.NETFramework,Version=v2.0)
      - net40 (.NETFramework,Version=v4.0)
      - portable-monoandroid10+monotouch10+net45+xamarinios10 (.NETPortable,Version=v0.0,Profile=net45+monoandroid10+monotouch10+xamarinios10)
      - portable-net45+sl5+win8 (.NETPortable,Version=v0.0,Profile=Profile47)
      - portable-net45+win8 (.NETPortable,Version=v0.0,Profile=Profile7)
      - portable-net45+win8+wp8 (.NETPortable,Version=v0.0,Profile=Profile78)
      - portable-net45+win8+wp8+wpa81 (.NETPortable,Version=v0.0,Profile=Profile259)
    One or more packages are incompatible with .NETCoreApp,Version=v1.0.

```


## Contributing and copyright

The project is hosted on [GitHub](https://github.com/Krzysztof-Cieslak/Suave.Kestrel) where you can [report issues](https://github.com/Krzysztof-Cieslak/Suave.Kestrel/issues), fork
the project and submit pull requests.

The library is available under [MIT license](https://github.com/Krzysztof-Cieslak/Suave.Kestrel/blob/master/LICENSE.md), which allows modification and
redistribution for both commercial and non-commercial purposes.
