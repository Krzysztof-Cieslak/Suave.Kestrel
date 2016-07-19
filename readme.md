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

## Contributing and copyright

The project is hosted on [GitHub](https://github.com/Krzysztof-Cieslak/Suave.Kestrel) where you can [report issues](https://github.com/Krzysztof-Cieslak/Suave.Kestrel/issues), fork
the project and submit pull requests.

The library is available under [MIT license](https://github.com/Krzysztof-Cieslak/Suave.Kestrel/blob/master/LICENSE.md), which allows modification and
redistribution for both commercial and non-commercial purposes.
