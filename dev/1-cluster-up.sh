#!/bin/bash

# create mongo persistent volume
mkdir -p /tmp/homelib/mongo

# get the logged in user's username
U=`logname`
# create the cluster and setup the cluster load balancer to handle traffic to the database service
k3d cluster create homelib -p 27017:30017@loadbalancer --volume /tmp/homelib/mongo:/tmp/homelib/mongo --volume $PWD/registries.yaml:/etc/rancher/k3s/registries.yaml --agents 1

# Fix ownership of kube config created by k3d
echo "Changing kube config owner to ${U}"
chown $U ~/.kube/config

docker volume create local_registry
docker container run -d --name registry.localhost -v local_registry:/var/lib/registry --restart always -p 5000:5000 registry:2

# Setup a route from the cluster to your docker registry
docker network connect k3d-homelib registry.localhost

