# Maui App + SignalR + OpenTelemetry + Grafana

## Overview

This repository demonstrates the integration flow from a .NET MAUI App managing real time with SignalR Core and sending telemetry to Prometheus and visualize the data in the Grafana visualization tool. 

The goal is to showcase how to monitor and visualize metrics from the MauiAppClient using industry-standard observability opentelemetry + prometheus + grafana tools.

## Prerequisites

- [.NET MAUI SDK](https://github.com/dotnet/maui#install-net-maui-sdk) installed
- [Grafana](https://grafana.com/docs/grafana/latest/getting-started/getting-started-prometheus/) installed and configured
- [OpenTelemetry](https://opentelemetry.io/docs/dotnet/getting-started/) setup for .NET
- [Prometheus](https://prometheus.io/docs/prometheus/latest/getting_started/) installed and configured

## Setup

1. Clone this repository:

   ```bash
   git clone https://github.com/your-username/maui-grafana-opentelemetry-prometheus.git
