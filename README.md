# Portal Shell Proof of Concept

A .NET 9 proof of concept that explores how a central portal can provide a consistent navigation experience across separate web applications.

The solution demonstrates two approaches:

- embedding a service inside the portal for comparison and experimentation
- navigating to a separate application while keeping a shared portal navigation experience

This repository is intended for architecture exploration and demonstration only. It is not a production implementation.

## Solution overview

The solution contains two ASP.NET Core MVC applications:

### PortalShell

`PortalShell` represents the central portal.

It provides:

- a Government of Canada-style header, navigation and footer
- a portal landing page
- sample portal navigation
- an embedded service demonstration
- a link to a separate connected application
- a reusable JavaScript Web Component for shared portal navigation

The shared navigation component is hosted at:

```text
PortalShell/wwwroot/js/portal-navigation.js
```

### ConnectedService

`ConnectedService` represents an independently deployed web application.

It demonstrates how a separate service can:

- keep its own application and routing
- use Government of Canada design elements
- load a shared navigation component supplied by the portal
- provide a clear way for the user to return to the portal

The sample landing page is presented as **Transport Canada AI Agent Services** to make the demonstration feel like a realistic standalone service.

## Architecture concept

The key idea is that participating services remain separate applications.

```text
+-----------------------+
|      PortalShell      |
|                       |
|  Portal navigation    |
|  Service directory    |
+-----------+-----------+
            |
            | normal browser navigation
            v
+-----------------------+
|   ConnectedService    |
|                       |
| Shared portal control |
| Service-specific UI   |
| Service-specific data |
+-----------------------+
```

The portal does not need to host the connected application's pages.

Instead, the portal can link to the application normally, while the connected application optionally loads a shared navigation component to provide a consistent way back to the portal.

## Connecting another web application

For the basic shared-navigation demonstration, a participating application only needs to add the shared component to its layout.

Example:

```html
<portal-navigation
    portal-url="https://portal.example"
    current-app="Connected service">
</portal-navigation>

<script src="https://portal.example/js/portal-navigation.js"></script>
```

The portal team then adds the application's URL to the portal navigation or service directory.

This is intentionally lightweight. Authentication, authorization and Single Sign-On are separate concerns.

## Single Sign-On

Single Sign-On is not implemented in this proof of concept.

A production solution could allow a user to sign in once and move between participating applications without repeatedly signing in, provided the applications are configured to use a common identity provider and compatible authentication flow.

Each connected application would still be responsible for enforcing its own authorization rules.

## Technology

- .NET 9
- ASP.NET Core MVC
- Government of Canada Web Template for MVC
- Government of Canada Design System components
- JavaScript Web Components
- Bootstrap in the default MVC project assets

## Repository structure

```text
Portal-Shell-POC/
|
+-- PortalShell/
|   +-- Controllers/
|   +-- Views/
|   +-- wwwroot/
|       +-- css/
|       +-- js/
|           +-- portal-navigation.js
|
+-- ConnectedService/
|   +-- Controllers/
|   +-- Views/
|   +-- wwwroot/
|
+-- Portal-Shell-POC.slnx
```

## Prerequisites

Install the .NET 9 SDK.

Check your installed version:

```powershell
dotnet --version
```

For local HTTPS development, trust the ASP.NET Core development certificate if needed:

```powershell
dotnet dev-certs https --trust
```

## Build

From the repository root:

```powershell
dotnet restore
dotnet build
```

## Run the proof of concept

Run each application in a separate terminal.

### PortalShell

```powershell
dotnet run --project PortalShell --launch-profile https
```

Open:

```text
https://localhost:7065
```

### ConnectedService

```powershell
dotnet run --project ConnectedService --launch-profile https
```

Open:

```text
https://localhost:7075
```

Both applications should be running at the same time so that `ConnectedService` can load the shared portal navigation component from `PortalShell`.

## Demonstrated scenarios

### Portal landing page

The portal provides a common shell with sample service areas and navigation.

### Embedded service

The project includes an iframe-based demonstration for comparing an embedded experience with normal browser navigation.

Embedding is not expected to work for every external service. Applications can prevent framing using browser security headers such as `X-Frame-Options` and Content Security Policy.

### Connected service

The connected-service demonstration uses normal browser navigation instead of an iframe.

This allows the destination application to remain independent while still presenting a shared portal control.

### Shared navigation component

`portal-navigation.js` defines a reusable custom HTML element.

The connected application loads that JavaScript file from the portal and uses the custom element in its own layout.

This demonstrates how a common portal control could be reused across independently deployed applications.

## Purpose

The goal of this repository is to answer a simple architecture question:

> Can a central portal provide a consistent experience across independent web applications without embedding every application inside the portal?

This proof of concept demonstrates that normal browser navigation combined with a lightweight shared navigation component is one possible approach.
