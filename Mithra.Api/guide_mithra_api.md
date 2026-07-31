# Mithra API (.NET) — Server Architecture

> **Version:** Draft v1
>
> **Goal:** Build a scalable, maintainable, and privacy-first backend for Mithra Observe, Lens, and Cortex.

---

# Philosophy

The backend should have one responsibility:

> **Receive, validate, store and analyse anonymous website events.**

It should **not** perform heavy processing during HTTP requests.

Everything expensive should happen asynchronously.

---

# Technology Stack

| Technology | Purpose |
|------------|---------|
| ASP.NET Core (.NET 10) | Web API |
| Entity Framework Core | ORM |
| PostgreSQL | Primary database |
| TimescaleDB *(future)* | Time-series optimisation |
| Redis *(future)* | Cache |
| BackgroundService | Background processing |
| System.Threading.Channels | In-memory queue |
| OpenTelemetry | Observability |
| Serilog | Logging |

---

# High-Level Architecture

```text
                Browser SDK
                     │
                     ▼
             ASP.NET Core API
                     │
             Request Validation
                     │
                     ▼
        System.Threading.Channel<T>
                     │
          Background Worker(s)
                     │
          Event Processing Pipeline
                     │
        ┌────────────┴────────────┐
        ▼                         ▼
   PostgreSQL              Lens Processing
                                      │
                                      ▼
                              Derived Signals
                                      │
                                      ▼
                                  Dashboard
```

---

# Solution Structure

```text
src/

├── Mithra.Api
├── Mithra.Application
├── Mithra.Domain
├── Mithra.Infrastructure
├── Mithra.Contracts
├── Mithra.Lens
├── Mithra.Cortex (future)
└── Mithra.Tests
```

---

# Project Responsibilities

## Mithra.Api

- REST API
- Authentication (future)
- Rate limiting
- Validation
- Request pipeline
- Swagger/OpenAPI

---

## Mithra.Application

Application use cases.

Examples

- Receive Events
- Create Website
- Query Analytics
- Generate Reports

Contains

- Commands
- Queries
- DTOs
- Validators
- Services

---

## Mithra.Domain

Business logic.

Contains

- Entities
- Value Objects
- Enums
- Interfaces
- Domain Events

No database code.

---

## Mithra.Infrastructure

Infrastructure implementations.

Contains

- EF Core
- PostgreSQL
- Repositories
- Background Workers
- Redis
- Logging
- File Storage

---

## Mithra.Contracts

Shared contracts.

Contains

- Request models
- Response models
- Event DTOs
- API version contracts

---

## Mithra.Lens

Behaviour analysis engine.

Responsibilities

- Feature extraction
- Behaviour detection
- Signal generation
- Metrics calculation

Lens should not know anything about HTTP.

---

## Mithra.Cortex

Future AI module.

Responsibilities

- AI summarisation
- Recommendations
- Predictions
- Anomaly detection

---

# API Design

```
POST   /api/v1/events
GET    /api/v1/websites
POST   /api/v1/websites
GET    /api/v1/dashboard
GET    /api/v1/metrics
GET    /api/v1/signals
```

Future

```
GET /api/v1/reports
GET /api/v1/recommendations
GET /api/v1/benchmarks
```

---

# Request Pipeline

```
HTTP Request

↓

Validation

↓

Website verification

↓

Consent verification (optional)

↓

Enqueue events

↓

Return HTTP 202 Accepted
```

Do **not** wait for database writes before responding.

---

# Event Processing Pipeline

```
Read batch

↓

Validate

↓

Normalise

↓

Store

↓

Generate Features

↓

Generate Signals

↓

Persist Results
```

---

# Background Workers

Recommended workers

## Event Worker

Stores incoming events.

---

## Lens Worker

Generates behaviour signals.

---

## Cleanup Worker

Deletes expired data.

---

## Aggregation Worker

Computes dashboard statistics.

---

## Benchmark Worker (future)

Generates anonymous benchmark data.

---

# Database

Main tables

```text
Websites

Sessions

Pages

Events

Features

Signals

Metrics
```

Future

```text
Recommendations

Benchmarks

Reports
```

---

# Event Storage

Events should be append-only.

Never update events after they are stored.

```
Incoming Event

↓

Insert

↓

Done
```

Derived data should live in separate tables.

---

# Event Processing

Raw Events

↓

Features

↓

Signals

↓

Metrics

↓

Dashboard

This keeps raw data immutable.

---

# Validation

Validate

- Website ID
- SDK Version
- Payload schema
- Timestamp
- Event type

Reject

- Invalid payload
- Oversized requests
- Unsupported versions

---

# Rate Limiting

Protect against

- Bots
- Accidental loops
- Abuse

Use ASP.NET Core Rate Limiting middleware.

---

# Logging

Log

- API errors
- Worker failures
- Queue size
- Processing duration

Never log

- Personal information
- Event payloads by default

---

# Observability

Measure

- Requests/sec
- Queue length
- Event processing time
- Failed inserts
- Database latency
- Lens processing time

Use OpenTelemetry from day one.

---

# Configuration

Example

```json
{
  "Database": {},
  "Queue": {},
  "Storage": {},
  "Privacy": {},
  "Lens": {},
  "Retention": {}
}
```

---

# Security

- HTTPS only
- CORS
- Input validation
- Request size limits
- Rate limiting
- Parameterized SQL (EF Core)
- Secure headers

No authentication is required for the event ingestion endpoint itself. Instead, each website should authenticate using a public **Website Key** that identifies the project. The server must validate this key before accepting events.

Administrative endpoints must require authentication and authorization.

---

# Data Retention

Configurable.

Examples

```
30 days

90 days

180 days

365 days
```

Cleanup should run automatically.

---

# Scalability Roadmap

## Version 1

```
API

↓

Channel<T>

↓

Background Worker

↓

PostgreSQL
```

---

## Version 2

```
Multiple Workers

↓

Redis Cache

↓

PostgreSQL
```

---

## Version 3

```
API

↓

Message Broker

↓

Worker Cluster

↓

PostgreSQL

↓

Lens

↓

Cortex
```

---

# Guiding Principles

- Keep HTTP requests fast.
- Process data asynchronously.
- Store immutable raw events.
- Generate derived analytics separately.
- Protect user privacy by design.
- Design for horizontal scaling.
- Keep business logic independent of infrastructure.
- Build modules that can evolve independently.

The server should remain a reliable data platform that powers **Observe**, **Lens**, and eventually **Cortex**, while maintaining a clean architecture and a privacy-first approach.