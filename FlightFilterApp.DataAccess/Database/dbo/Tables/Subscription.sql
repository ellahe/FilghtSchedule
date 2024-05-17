CREATE TABLE [dbo].[Subscription] (
    [Id]                INT IDENTITY (1, 1) NOT NULL,
    [AgencyId]          INT NULL,
    [OriginCityId]      INT NULL,
    [DestinationCityId] INT NULL
);


GO
CREATE NONCLUSTERED INDEX [idx_subscription_agencyid]
    ON [dbo].[Subscription]([AgencyId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_subscription_originCityId]
    ON [dbo].[Subscription]([DestinationCityId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_subscription_destinationCityId]
    ON [dbo].[Subscription]([DestinationCityId] ASC);

