<p align="center">
<img alt="LH" src="https://littlehorse.dev/img/logo.jpg" width="50%">
</p>

# LittleHorse Python Run Workflow

- [LittleHorse Python Run Workflow](#littlehorse-python-run-workflow)
- [Prerequisites](#prerequisites)
 	- [Python Setup](#python-setup)
- [Register Workflow](#register-workflow)

# Prerequisites

Your system needs:

- `python` 3.9 or later

## Python Setup

We need a python environment that has the `littlehorse-client` pip package. We recommend making a python virtual environment. To install the `littlehorse` package, you can use a package manager of your choice. We show you below how to do it with `pip` or `poetry`:

The first option is to install via `pip`:

```sh
pip install typing_extensions
pip install littlehorse-client
```

Alternatively, you can install via `poetry` using our `pyproject.toml` file as follows:

```sh
poetry install
poetry shell
```

After installing our Python SDK via your preferred method, you should be able to import the `littlehorse` python package:

```sh
-> python3
>>> import littlehorse
>>>
```

# Register Workflow

First, we run `register.py`, which does two things:

1. Registers a `TaskDef` named `greet` with LittleHorse.
2. Registers a `WfSpec` named `quickstart` with LittleHorse.

Locate in the workflow folder

```sh
cd workflow
```

A [`WfSpec`](https://littlehorse.dev/docs/concepts/workflows) specifies a process which can be orchestrated by LittleHorse. A [`TaskDef`](https://littlehorse.dev/docs/concepts/tasks) tells LittleHorse about a specification of a task that can be executed as a step in a `WfSpec`.

```sh
python3 -m register
```

You can inspect your `WfSpec` with `lhctl` as follows. It's ok if the response doesn't make sense, we will see it soon!

```sh
lhctl get wfSpec quickstart
```

Now, go to your dashboard in your browser (`http://localhost:8080`) and refresh the page. Click on the `quickstart` WfSpec. You should see something that looks like a flow-chart. That is your Workflow Specification!
