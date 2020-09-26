#!/bin/bash

############################################################################
# Deploy Services
############################################################################

kubectl apply -f ./k8s/mongo-vols.yaml
kubectl create -f ./k8s/mongo-deploy.yaml

############################################################################
# Prepare database
############################################################################

JOB_IMG=data_loader_img
RUN_ENV=staging

#docker rmi registry.localhost:5000/$JOB_IMG:$RUN_ENV
#docker rmi $JOB_IMG:$RUN_ENV
#docker rmi $JOB_IMG

mount --bind ../src src
mount --bind ../data data
docker build --build-arg run_environment="${RUN_ENV}" -t $JOB_IMG -f ./docker/Dockerfile_dataloader .
umount src
umount data

docker tag $JOB_IMG:latest $JOB_IMG:$RUN_ENV
docker tag $JOB_IMG:$RUN_ENV registry.localhost:5000/$JOB_IMG:$RUN_ENV
docker push registry.localhost:5000/$JOB_IMG:$RUN_ENV

kubectl apply -f ./k8s/dataloader-job.yaml
kubectl wait --for=condition=complete --timeout=60s job/dataloader
jobpod=$(kubectl get pods --selector=job-name=dataloader --output=jsonpath='{.items[*].metadata.name}')
echo $jobpod
kubectl logs $jobpod
kubectl delete job/dataloader
