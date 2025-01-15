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
  - [Run the Task Worker](#run-the-task-worker)
  - [Register the `WfSpec`](#register-the-wfspec)
    - [Using `lhctl`](#using-lhctl)
    - [Using Python](#using-python)
  - [Running the `WfSpec`](#running-the-wfspec)
  - [Getting In Touch](#getting-in-touch)

**Get started in under 5 minutes, or your money back!** :wink:

This repo contains a minimal example to get you started using LittleHorse in dotnet. [LittleHorse](www.littlehorse.dev) is a high-performance orchestration engine which lets you build workflow-driven microservice applications with ease.

You can run this example in two ways:

1. Using a local deployment of a LittleHorse Server (instructions below, requires one `docker` command).
2. Using a LittleHorse Server deployed in a cloud sandbox (to get one, contact `info@littlehorse.io`).

In this example, we will run a classic "Greeting" workflow as a quickstart. The workflow takes in one input variable (`input-name`), and calls a `greet` Task Function with the specified `input-name` as input.

## Prerequisites

Your system needs:
* `dotnet` 6.0 or later (please make sure that the `lh-quickstart-dotnet.csproj` file matches your version)
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

```
docker run --name littlehorse -d -p 2023:2023 -p 8080:8080 ghcr.io/littlehorse-enterprises/littlehorse/lh-standalone:0.12.2
```

Using the local LittleHorse Server takes about 15-25 seconds to start up, but it does not require any further configuration. Please note that the `lh-standalone` docker image requires at least 1.5GB of memory to function properly. This is because it runs kafka, the LH Server, and the LH Dashboard (2 JVM's and a NextJS app) all in one container.

## Verifying Setup

At this point, whether you are using a local Docker deployment or a private LH Cloud Sandbox, you should be able to contact the LH Server:

```
->lhctl version
lhctl version: 0.12.2 (Git SHA homebrew)
Server version: 0.12.2
```

**You should also be able to see the dashboard** at `https://localhost:8080`. It should be empty, but we will put some data in there soon when we run the workflow!

If you _can't_ get the above to work, please let us know at `info@littlehorse.io`, or send us a message on our [Slack Community](https://launchpass.com/littlehorse-community).

## Running the Example

To run this example, we will do three things:

1. Run the dotnet portion of this project, which registers a `TaskDef` and starts a Task Worker to poll tasks.
2. Create a `WfSpec` which _uses_ our `TaskDef`.
3. Run the `WfSpec` using `lhctl` and see our first `WfRun` in action.

**NOTE:** we are currently developing an SDK to build a `WfSpec` in DotNet, but it is not yet released. In the meantime, you have two options for registering your `WfSpec` in this project:

1. Using `lhctl` and the [`put-wfspec-request.json`](./put-wfspec-request.json) file.
2. Using our Java, Python, or GoLang SDK's.

For more information about developing `WfSpec`s, please refer to [WfSpec Development documentation](https://littlehorse.io/docs/server/developer-guide/wfspec-development/basics).

## Run the Task Worker

We will run our dotnet project, which does two things:

1. Create a `TaskDef` named `greet` from the `Greeting()` method.
2. Start a Task Worker polling on the `greet` Task Queue.

When we run our `WfSpec` (the last step), LittleHorse will send a `TaskRun` to be executed by the Task Worker.

```
dotnet build
dotnet run
```

## Register the `WfSpec`

In LittleHorse, you must register a `WfSpec` before you can run a `WfRun` (for more information, check out [our documentation](https://littlehorse.io/docs/server/concepts/workflows)).


### Using `lhctl`

The `put-wfspec-request.json` file included in this quickstart is a simple `WfSpec` that just executes our `greet` task. Since we are currently developing the `WfSpec` SDK for DotNet, the easiest way for you to register a `WfSpec` is as follows:

```
lhctl deploy wfSpec put-wfspec-request.json
```

### Using Python

Alternatively, in the [`workflow` directory](./workflow/), we have written code to define a `WfSpec` using our Python SDK. You can follow the [README](./workflow/README.md) there.

Alternatively, you can check out our [WfSpec Development documentation](https://littlehorse.io/docs/server/developer-guide/wfspec-development/basics) to use the Go or Java SDK's.

## Running the `WfSpec`

To run the `WfSpec`, you can run the following command in another terminal:

```
lhctl run quickstart input-name obiwan
```

Then open the dashboard, click on the `quickstart` WfSpec, and inspect your `WfRun`. Congrats!

## Getting In Touch

Congrats on your first DotNet Task Worker! To learn more:

- Dive deeper with our [Documentation](https://littlehorse.io/docs/server)
- Join us [on Slack](https://launchpass.com/littlehorsecommunity)
- Give us a star [on GitHub](https://github.com/littlehorse-enterprises/littlehorse)!

Happy riding!
