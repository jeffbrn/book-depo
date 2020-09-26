#!/bin/bash

docker network disconnect k3d-bookrepo registry.localhost

k3d cluster delete bookrepo
rm -r /tmp/bookrepo

docker container stop registry.localhost
docker container rm registry.localhost
docker volume rm local_registry

