.PHONY: build-release build-debug run-release run-debug

ARGS ?= ""

build-release:
	dotnet build -c Release

build-debug:
	dotnet build -c Debug

run-release: build-release
	./bin/Release/net10.0/loxinas $(ARGS)

run-debug: build-debug
	./bin/Debug/net10.0/loxinas $(ARGS)
