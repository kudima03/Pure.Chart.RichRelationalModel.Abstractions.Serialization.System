# Changelog

All notable changes to Pure.Chart.RichRelationalModel.Abstractions.Serialization.System
are documented here.

Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

---

## [0.1.0-preview.2.0.1] — 2026-06-07

### Changed

- Maintenance release: dependency and build updates.

## [0.1.0-preview.2.0.0] — 2026-04-26

### Changed

- **`AxisRichRelationalModelConverter`** no longer reads or writes a
  `ChartId` property, matching the `IAxisRichRelationalModel` contract in
  `Pure.Chart.RichRelationalModel.Abstractions`. JSON previously produced
  by this converter that includes `chartId` is no longer compatible.

## [0.1.0-preview.1.0.0] — 2026-04-20

### Changed

- **`SeriesRichRelationalModelConverter`** renamed to
  **`ChartSeriesRichRelationalModelConverter`**, and now converts
  `IChartSeriesRichRelationalModel` instead of `ISeriesRichRelationalModel`,
  following the corresponding rename upstream in
  `Pure.Chart.RichRelationalModel.Abstractions`.
- **`ChartRichRelationalModelConverter`** updated accordingly: the `Series`
  property on the deserialized model is now
  `IEnumerable<IChartSeriesRichRelationalModel>`.
- **`ChartRichRelationalModelAbstractionsConverters`** now yields a
  `ChartSeriesRichRelationalModelConverter` in place of the removed
  `SeriesRichRelationalModelConverter`.

## [0.1.0-preview.0.1.0] — 2026-04-06

### Added

Initial release. `System.Text.Json` converters for the
`Pure.Chart.RichRelationalModel.Abstractions` model types:

- **`AxisRichRelationalModelConverter`** — converts `IAxisRichRelationalModel`
  (`Id`, `ChartId`, `Legend`).
- **`ChartTypeRichRelationalModelConverter`** — converts
  `IChartTypeRichRelationalModel` (`Id`, `Name`).
- **`SeriesRichRelationalModelConverter`** — converts
  `ISeriesRichRelationalModel` (`Id`, `ChartId`, `Legend`, `XAxisSource`,
  `YAxisSource`).
- **`ChartRichRelationalModelConverter`** — converts
  `IChartRichRelationalModel` (`Id`, `Title`, `Description`, `TypeId`,
  `Type`, `XAxisId`, `XAxis`, `YAxisId`, `YAxis`, `Series`), composing the
  converters above.
- **`ChartRichRelationalModelAbstractionsConverters`** — an
  `IEnumerable<JsonConverter>` bundling all of the above converters for
  registration with `JsonSerializerOptions`.
