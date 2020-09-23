#!/bin/bash

kubectl delete -f ./k8s/mongo-deploy.yaml
kubectl delete -f ./k8s/mongo-vols.yaml

