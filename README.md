# GovernmentExpenses.ca [Government Expenses in Canada](https://governmentexpenses.ca)

GovernmentExpenses.ca is a legacy transparency project that aggregates Canadian federal travel and hospitality disclosures into a searchable website. The public-facing site explains that the project started after its author found official proactive-disclosure pages too fragmented and time-consuming to analyze by hand, and Maclean's later highlighted the site as an important contribution to public accountability.

## What this repository contains

This repository appears to be the source for the original GovernmentExpenses.ca stack:

- `butterfly/` — the main ASP.NET Web Forms site that presents reports, rankings, department pages, and paid database downloads
- `caterpillar/` — a .NET crawler/parser that walks government disclosure sites, extracts travel and hospitality records, and stores them in SQL Server
- `webfront/` — a small Web Forms control surface for running the crawler and updating cache data
- `cache/` — a lightweight crawler utility for mirroring site content
- `validate/` — a helper utility for validating person-name data

## Project purpose

The code and bundled site content describe a project built to:

- consolidate scattered proactive-disclosure pages into one place
- tabulate totals and trends across departments and individuals
- make government spending easier for the public, journalists, and researchers to inspect
- preserve source fidelity by reproducing published data and linking back to source URLs

The site content also states that all data was reproduced from official Canadian government disclosures and that obvious source-data issues were flagged rather than silently rewritten.

## Data model and workflow

At a high level, the application works like this:

1. `caterpillar/` loads XML crawler definitions from files such as `config.xml`, `rootpages.xml`, and `types.xml`.
2. It crawls department disclosure pages and PDFs using many department-specific regular expressions.
3. Parsed travel and hospitality records are written into SQL Server tables.
4. `butterfly/` reads that database and renders public reports and search pages.

The crawler configuration shows support for many departments and agencies, plus both HTML and PDF-based disclosure formats.

## Technology snapshot

- ASP.NET Web Forms
- C# / .NET Framework 4.0
- SQL Server
- XML-driven crawler configuration
- PDF parsing via PDFsharp

## Repository status

This is a historical codebase and still contains many legacy assumptions from its original environment:

- The public site content references data last retrieved on **2011-01-10**.
- The web application targets classic Visual Studio Web Application tooling.
- The crawler and site expect SQL Server-backed configuration.
- Checked-in configuration files use historical connection-string values that should be replaced locally before any real use.

## Building

This repository does not currently build cleanly in the provided Linux environment:

- `dotnet build /home/runner/work/GovernmentExpenses.ca/GovernmentExpenses.ca/Caterpillar.sln` fails because the solution references `Validate/Validate.csproj` while the repository directory is lowercase `validate/`.
- `dotnet build /home/runner/work/GovernmentExpenses.ca/GovernmentExpenses.ca/Butterfly.sln` fails because the legacy Web Forms project expects Visual Studio web application targets that are not available in the current SDK image.

In practice, this project likely needs a Windows machine with legacy Visual Studio/MSBuild support and a local SQL Server instance to run as originally designed.

## Source material and context

Useful background links:

- GovernmentExpenses.ca about page (mirrored in `butterfly/about.aspx`) [About](https://governmentexpenses.ca/about.html)
- Maclean's: [Information needs to be free](https://www.macleans.ca/politics/ottawa/information-needs-to-be-free/)
- Maclean's: [High-flying civil servants](https://www.macleans.ca/news/canada/high-flying-civil-servants/)
- Maclean's: [Is public data the future of governance?](https://www.macleans.ca/society/technology/power-to-the-people/)
- Government of Canada Open Government portal: https://open.canada.ca/en

## Notes for maintainers

- Treat the XML files in `caterpillar/` as the crawler's main operating configuration.
- Avoid reusing checked-in credentials or connection strings.
- Expect department-specific scraper logic and historical site quirks throughout the parser definitions.
