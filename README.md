# Cogworks.ContentGuard &middot; [![GitHub license](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](LICENSE.md) [![Github Build](https://img.shields.io/github/workflow/status/thecogworks/cogworks.contentguard/Changelog%20generator%20and%20NuGet%20Releasing)](https://github.com/thecogworks/Cogworks.ContentGuard/actions?query=workflow%3A%22Changelog+generator+and+NuGet+Releasing%22) [![NuGet Version](https://img.shields.io/nuget/v/Cogworks.ContentGuard)](https://www.nuget.org/packages/Cogworks.ContentGuard/) [![Our Umbraco project page](https://img.shields.io/badge/our-umbraco-orange.svg)]() [![codecov](https://codecov.io/gh/thecogworks/Cogworks.ContentGuard/branch/master/graph/badge.svg?token=UMLJ5S8UJX)](undefined)

A package for Umbraco CMS allowing to seamleassly lock down pages for the specific editor to avoid overwriting content with simultanous changes.

## Getting started

This package is supported on Umbraco 8.9+.

## Installation

Cogworks.ContentGuard is available to be downloaded from Our Umbraco, NuGet or as a manual download directly from GitHub.

### NuGet package repository

To [install from NuGet](https://www.nuget.org/packages/Cogworks.ContentGuard), you can run the following command from within Visual Studio:

```
PM> Install-Package Cogworks.ContentGuard
```

## Usage

After installing the package, you'll see an additional tab ([Content App](https://our.umbraco.com/documentation/extending/Content-Apps/)) added for the Content section pages called **"Content Guard"**.

![](docs/img/cg-example-1.jpeg?raw=true)

When accessing locked page, you'll be **prompt** with the appropriate modal window and option to choose the action - take over or leave the page.

![](docs/img/cg-example-2.jpeg?raw=true)

To unlock the page and enable other editors access it without prompts and take overs, when the editor's job is done, **the "Unlock" button** may remove the lock from the current page.

![](docs/img/cg-example-3.jpeg?raw=true)

## Contribution guidelines

To raise a new bug, create an issue on the GitHub repository. To fix a bug or add new features, fork the repository and send a pull request with your changes. Feel free to add ideas to the repository's issues list if you would to discuss anything related to the package.

Demo/Dev Umbraco Web App (Cogworks.ContentGuard.Web) - backoffice credentials:
- Usernames: admin@demo.com, admin2@demo.com
- Password: password123

## License
  
Cogworks.ContentGuard is licensed under the [Apache License, Version 2.0](https://opensource.org/licenses/Apache-2.0)