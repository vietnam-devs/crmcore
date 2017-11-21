<p align="center">
  <img align="center" class="image" src="https://github.com/crm-core/crmcore/blob/master/art/logo.png" width="150px">  
</p>

<p align="center">
  <a href="https://ci.appveyor.com/project/tungphuong/crmcore/branch/master">
    <img src="https://img.shields.io/appveyor/ci/tungphuong/crmcore/master.svg?label=appveyor&style=flat-square" alt="Build status" data-canonical-src="https://img.shields.io/appveyor/ci/tungphuong/crmcore/master.svg?label=appveyor&amp;style=flat-square" style="max-width:100%;"></a>
  <a href="https://travis-ci.org/crm-core/crmcore"><img src="https://travis-ci.org/crm-core/crmcore.svg?label=travis-ci&branch=master&style=flat-square" alt="Build Status" data-canonical-src="https://travis-ci.org/crm-core/crmcore.svg?label=travis-ci&branch=master" style="max-width:100%;"></a>
  <a href="https://github.com/crm-core/crmcore/blob/master/LICENSE"><img src="https://img.shields.io/badge/price-FREE-0098f7.svg" alt="Price"></a>
</p>

----
<p align="center">
<img width='46px' src="http://browserbadge.com/ie/9">
<img width='46px' src="http://browserbadge.com/opera/20">
<img width='46px' src="http://browserbadge.com/safari/6">
<img width='46px' src="http://browserbadge.com/firefox/28">
<img width='46px' src="http://browserbadge.com/chrome/39">
</p>

**CRM-Core** makes it easy to create and manage a **Lightweight-CRM Web Application** efficiently.

## Table of contents
- [Quick start](https://github.com/crm-core/crmcore#quick-start)
- [What's included](https://github.com/crm-core/crmcore#whats-included)
- [Bugs and feature requests](https://github.com/crm-core/crmcore#bugs-and-feature-requests)
- [Dependencies](https://github.com/crm-core/crmcore#dependencies)
- [Community](https://github.com/crm-core/crmcore#community)
- [Development](https://github.com/crm-core/crmcore#development)
- [Contributors](https://github.com/crm-core/crmcore#contributors)
- [Copyright and license](https://github.com/crm-core/crmcore#copyright-and-license)

## Quick start

### Real world demo

You can access to the application on Azure as following links:
- http://52.187.186.33
- http://52.187.186.33/swagger
- http://52.187.186.33/docs

### Docker

#### Linux / Unix

```bash
docker run -p 80:5000 --name crmcore  crmcore/crm-linux
```

#### Windows

```bash
docker run -p 80:5000 --name crmcore  crmcore/crm-window
```

For more information, you can check out [CRM Core on Docker Hub](https://hub.docker.com/u/crmcore)

### Manual

- Download the latest .NET SDK (2.x) & NodeJS
- Clone the repo: `git clone https://github.com/crm-core/crmcore.git`
- Change location to `\crmcore\src\Hosts\CRMCore.WebApp`, then run command `dotnet run`

## What's included

*Coming soon...*

## Bugs and feature requests
Have a bug or a feature request? Please first read the issue guidelines and search for existing and closed issues. If your problem or idea is not addressed yet, [please open a new issue](https://github.com/crm-core/crmcore/issues/new).

## Dependencies

- [.NET Core SDK 2.0](https://www.microsoft.com/net/download)
- [Create-React-App](https://github.com/facebookincubator/create-react-app)
- [React CoreUI](https://github.com/mrholek/CoreUI-React)

## Community
Get updates on CRMCore' development and chat with the project maintainers and community members:
- Follow [Phuong Le on GitHub](https://github.com/tungphuong)
- Follow [@thangchung on Twitter](https://twitter.com/thangchung)

## Development

### Get code

```bash
git clone git@github.com:crm-core/crmcore.git
cd crmcore
```

### Back-end Development environment

**You’ll need to have .NET SDK 2.x on your machine.**

It will be organize the initial project structure and install the transitive dependencies:

```
crm-core
├── README.md
├── LICENSE
├── .gitignore
├── global.json
├── Dockerfile
├── .travis.yml
├── appveyor.yml
├── crmcore.sln
├── art
└── src
    └── crm
        └── CRMCore.Module.Common
        └── CRMCore.Module.Contact
        └── CRMCore.Module.Setup
        └── CRMCore.Module.Spa
        └── Directory.Build.props
    └── framework
        └── CRMCore.Framework.CqrsLite
        └── CRMCore.Framework.Entities
        └── CRMCore.Framework.MvcCore
    └── hosts
        └── CRMCore.ClientApp
        └── CRMCore.WebApp
    └── modules
        └── CRMCore.Module.Communication
        └── CRMCore.Module.Data
        └── CRMCore.Module.Identity
    └── targets
        └── CRMCore.Application.Crm.targets
        └── CRMCore.Application.Targets
        └── CRMCore.Module.Targets
        └── CRMCore.Theme.Targets
    └── themes
        └── CRMCore.Theme
```

```bash

cd <your path>crmcore\src\hosts\CRMCore.WebApp
dotnet restore
dotnet run

```

### Front-end Development environment

**You’ll need to have Node >= 6 on your machine.**

It will organize the initial project structure and install the transitive dependencies:

```
crm-core\src\hosts\CRMCore.ClientApp
├── node_modules
├── package.json
├── yarn.lock
├── .gitignore
├── public
│   └── favicon.ico
│   └── index.html
│   └── manifest.json
└── src
    └── components
    └── configs
    └── containers
    └── redux
        └── middlewares
        └── modules
    └── styles
        └── bootstrap
        └── core
        └── vendors
        └── images
        └── style.scss
    └── index.js
    └── logo.svg
    └── registerServiceWorker.js
```

We need several packages that were installed in global scope as following commands

```bash

npm i react-scripts npm-run-all cpx node-sass-chokidar -g

```

Then, we can run following commands

```bash

cd <your path>crmcore\src\hosts\CRMCore.ClientApp
yarn install
yarn start

```

> When you build **CRMCore.WebApp**, then it will call MSBuild script inside to automatically build the assets for the front-end.
> You need to remove the **index.html** inside wwwroot folder to make it happen.

## Contributors

*N/A*


## Copyright and license

Code and documentation copyright 2017 [CRMCore](https://github.com/crm-core). Code released under the [MIT License](https://github.com/crm-core/crmcore/blob/master/LICENSE).
