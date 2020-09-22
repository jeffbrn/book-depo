#!/bin/bash

docker network disconnect k3d-homelib registry.localhost

k3d cluster delete homelib
rm -r /tmp/homelib

docker container stop registry.localhost
docker container rm registry.localhost
docker volume rm local_registry

