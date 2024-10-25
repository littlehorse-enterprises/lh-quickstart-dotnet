<p align="center">
<img alt="LH" src="https://littlehorse.dev/img/logo.jpg" width="50%">
</p>

# LittleHorse Dotnet QuickStart

- [LittleHorse Dotnet QuickStart](#littlehorse-dotnet-quickstart)
- [Prerequisites](#prerequisites)
    - [Dotnet Setup](#dotnet-setup)
    - [LittleHorse CLI](#littlehorse-cli)
    - [Local LH Server Setup](#local-lh-server-setup)
    - [Verifying Setup](#verifying-setup)
- [Running the Example](#running-the-example)
    - [Register and Run Task Worker](#register-and-run-task-worker)
- [More Information](#more-information)

**Get started in under 5 minutes, or your money back!** :wink:

This repo contains a minimal example to get you started using LittleHorse in dotnet. [LittleHorse](www.littlehorse.dev) is a high-performance orchestration engine which lets you build workflow-driven microservice applications with ease.

You can run this example in two ways:

1. Using a local deployment of a LittleHorse Server (instructions below, requires one `docker` command).
2. Using a LittleHorse Server deployed in a cloud sandbox (to get one, contact `info@littlehorse.io`).

In this example, we will run a classic "Greeting" workflow as a quickstart. The workflow takes in one input variable (`input-name`), and calls a `greet` Task Function with the specified `input-name` as input.

# Prerequisites

Your system needs:
* `dotnet` 6.0 or later
* `brew` (to install `lhctl`). This has been tested on Linux and Mac.

## Dotnet Setup

If you are using a Debian Linux SO you can install dotnet through

```
sudo apt-get update &&   sudo apt-get install -y dotnet-sdk-8.0 
```

Alternatively, you can install dotnet using `brew`:

```
brew install --cask dotnet
```

## LittleHorse CLI

Install the LittleHorse CLI:

```
brew install littlehorse-enterprises/lh/lhctl
```

## Local LH Server Setup

If you have obtained a private LH Cloud Sandbox, you can skip this step and just follow the configuration instructions you received from the LittleHorse Team (remember to set your environment variables!).

To run a LittleHorse Server locally in one command, you can run:

To run a LittleHorse Server locally in one command, you can run:

```
docker run --name littlehorse -d -p 2023:2023 -p 8080:8080 ghcr.io/littlehorse-enterprises/littlehorse/lh-standalone:0.11.2
```

Using the local LittleHorse Server takes about 15-25 seconds to start up, but it does not require any further configuration. Please note that the `lh-standalone` docker image requires at least 1.5GB of memory to function properly. This is because it runs kafka, the LH Server, and the LH Dashboard (2 JVM's and a NextJS app) all in one container.

## Verifying Setup

At this point, whether you are using a local Docker deployment or a private LH Cloud Sandbox, you should be able to contact the LH Server:

```
->lhctl version
lhctl version: 0.11.2 (Git SHA homebrew)
Server version: 0.11.2
```

If you _can't_ get the above to work, please let us know at `info@littlehorse.io`. We will create a community slack for support soon.

**You should also be able to see the dashboard** at `https://localhost:8080`. It should be empty, but we will put some data in there soon when we run the workflow!

If you _can't_ get the above to work, please let us know at `info@littlehorse.io`, or send us a message on our [Slack Community](https://launchpass.com/littlehorse-community).

# Running the Example

Without further ado, let's run the example start-to-finish.

## Register and Run Task Worker

First, let's register the taskDef and start our worker, so that our blocked `WfRun` can finish:

```
cd lh-quickstart-dotnet
dotnet build
dotnet run
```

Once the worker starts up, please open another terminal and inspect our `WfRun` again:

```
lhctl get wfRun <wf_run_id>
```

Voila! It's completed. You can also verify that the Task Queue is empty now that the Task Worker executed all of the tasks:

```
lhctl search taskRun --taskDefName greet --status TASK_SCHEDULED
```

Please refresh the dashboard, and you can see the `WfRun` has been completed!

## More Information

We also have quickstarts in [Java](https://github.com/littlehorse-enterprises/lh-quickstart-java) and [Go](https://github.com/littlehorse-enterprises/lh-quickstart-go). Support for .NET is coming soon.

Our extensive [documentation](www.littlehorse.dev) explains LittleHorse concepts in detail and shows you how take full advantage of our system.

Our LittleHorse Server is free for production use under the SSPL license. You can find our official docker image at the [AWS ECR Public Gallery](https://gallery.ecr.aws/littlehorse/lh-server). If you would like enterprise support, or a managed service (either in the cloud or on-prem), contact `info@littlehorse.io`.

Happy riding!
