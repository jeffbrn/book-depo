# Project Dev Stack

## Dev Stack Requirements

First docker must be [installed](https://docs.docker.com/engine/install/ubuntu)

For K3D installation instructions see [here](https://k3d.io/#installation)

You will need kubectl [installed](https://kubernetes.io/docs/tasks/tools/install-kubectl/)

## Helper Scripts

* 1-cluster-up.sh - Spins up the virtual cluster with a single master and a single node along with a loadbalancer for external access to mongo and a PV for mongo.
    It then spins up a docker registry used to deploy our built applications.
* 2-deploy-svcs.sh - Spins up the mongo database and then builds, deploys and runs the DataLoader app to seed the database with test data and waits for it to finish.
* 3-del-svcs.sh -  Tears down the database service.
* 4-cluster-down.sh - Tears down the virtual cluster.


## Warning

If you change the DataLoader application in './src/DataLoader/DataLoader.sln', then the cluster needs to be torn down and rebuilt in 
order for kuberenetes to recognize the application changes0. Otherwise the old version will run.
