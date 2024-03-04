# Maui App + SignalR + OpenTelemetry + Prometheus + Grafana

## Overview

This repository demonstrates the integration flow from a .NET MAUI App simulating real time stocks information with SignalR Core and sending that telemetry to Prometheus and visualize the data in the Grafana visualization tool. 

The goal is to showcase how to monitor and visualize metrics from the MauiAppClient using industry-standard observability opentelemetry + prometheus + grafana tools.

## Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/community/) installed.
- [Prometheus](https://prometheus.io/download/) installed and configured.
- [Grafana](https://prometheus.io/docs/tutorials/visualizing_metrics_using_grafana/) installed and the prometheus datasource configured.

## Setup

1. Clone o download this repository:

   ```bash
   git clone https://github.com/milocko07/maui-signalr-opentelemetry
2. Optionally configure prometheus.yml that you have downloaded from [Prometheus](https://prometheus.io/download/) web page with your custom configuration (port and times). [This is](https://github.com/milocko07/maui-signalr-opentelemetry/blob/main/prometheus.yml) a copy of the one that is used in this project.
3. Run prometheus.exe file from the same folder that you have downloaded from the web page.
4. Open and Run [SignalRServer project](https://github.com/milocko07/maui-signalr-opentelemetry/tree/main/SignalRServer/).
5. Open, configure SignalRServer URL and run [ConsoleClientApp project](https://github.com/milocko07/maui-signalr-opentelemetry/tree/main/ConsoleAppClient).
6. Open, configure SignalRServer URL and run [MauiAppClient](https://github.com/milocko07/maui-signalr-opentelemetry/tree/main/MauiAppClient)
7. You should see next similar results:
  <img width="765" alt="image" src="https://github.com/milocko07/maui-signalr-opentelemetry/assets/37205551/59cde85b-6cb9-45a0-8d18-e0bccf700368">
  <img width="564" alt="image" src="https://github.com/milocko07/maui-signalr-opentelemetry/assets/37205551/52c1a12d-3e3d-4bdf-a533-9ad6d99eee07">
  <img width="275" alt="image" src="https://github.com/milocko07/maui-signalr-opentelemetry/assets/37205551/21ab37a4-9e1d-4399-92d7-6ee87acad034">
  <img width="959" alt="image" src="https://github.com/milocko07/maui-signalr-opentelemetry/assets/37205551/b4bb55ff-9956-4dd1-af04-3fb07e1dd1ee">

8. And now you should see the telemetry data in Grafana http://localhost:3000/explore? url:
  <img width="956" alt="image" src="https://github.com/milocko07/maui-signalr-opentelemetry/assets/37205551/b1bc9604-830e-4574-9e2d-6d4a8ccf2c0f">





