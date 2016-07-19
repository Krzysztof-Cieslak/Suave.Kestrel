#!/bin/bash

#kestrel
wrk -t12 -c400 -d30s http://localhost:8080 | tee kestrelbench.txt
#core clr
wrk -t12 -c400 -d30s http://localhost:8081 | tee coreclrbench.txt
#mono
wrk -t12 -c400 -d30s http://localhost:8083 | tee monobench.txt
#mono on libuv
wrk -t12 -c400 -d30s http://localhost:8084 | tee monobenchonlibuv.txt