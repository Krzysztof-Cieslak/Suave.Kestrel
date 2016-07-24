#!/bin/bash

echo "Benchmarking Suave on Kestrel"
#kestrel
wrk -t12 -c400 -d30s http://localhost:8080 | tee suaveOnKestrel.txt
echo "Benchmarking Suave On CoreCLR"
#core clr
wrk -t12 -c400 -d30s http://localhost:8081 | tee suaveOnCoreCLR.txt
echo "Benchmarking Suave on Mono"
#mono
wrk -t12 -c400 -d30s http://localhost:8083 | tee suaveOnMono.txt
echo "Benchmarking Kestrel plain"
#mono on libuv
wrk -t12 -c400 -d30s http://localhost:8085 | tee kestrelPlain.txt