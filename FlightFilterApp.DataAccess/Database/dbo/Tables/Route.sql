CREATE TABLE [dbo].[Route] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [RouteId]           INT           NOT NULL,
    [OriginCityId]      INT           NULL,
    [DestinationCityId] INT           NULL,
    [Departure_date]    DATETIME2 (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([RouteId] ASC),
    CONSTRAINT [IX_Route_UniqueConstraint] UNIQUE NONCLUSTERED ([OriginCityId] ASC, [DestinationCityId] ASC, [RouteId] ASC)
);

