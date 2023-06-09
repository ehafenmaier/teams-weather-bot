﻿using WeatherBot.Models;
using AdaptiveCards.Templating;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.TeamsFx.Conversation;
using Newtonsoft.Json;
using WeatherBot.AzureMaps;

namespace WeatherBot.Commands
{
    /// <summary>
    /// The <see cref="HelloWorldCommandHandler"/> registers a pattern with the <see cref="ITeamsCommandHandler"/> and 
    /// responds with an Adaptive Card if the user types the <see cref="TriggerPatterns"/>.
    /// </summary>
    public class HelloWorldCommandHandler : ITeamsCommandHandler
    {
        private readonly ILogger<HelloWorldCommandHandler> _logger;
        private readonly IAzureMapsService _azureMapsService;
        private readonly string _adaptiveCardFilePath = Path.Combine(".", "Resources", "HelloWorldCard.json");

        public IEnumerable<ITriggerPattern> TriggerPatterns => new List<ITriggerPattern>
        {
            // Used to trigger the command handler if the command text contains 'helloWorld'
            new RegExpTrigger("helloWorld")
        };

        public HelloWorldCommandHandler(
            ILogger<HelloWorldCommandHandler> logger,
            IAzureMapsService azureMapsService)
        {
            _logger = logger;
            _azureMapsService = azureMapsService;
        }

        public async Task<ICommandResponse> HandleCommandAsync(ITurnContext turnContext, CommandMessage message, CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation($"Bot received message: {message.Text}");

            var location = await _azureMapsService.GetLocationByAddressPartsAsync(new AddressParts
            {
                City = "Salt Lake City",
                StateProvince = "UT"
            });

            var weather = await _azureMapsService.GetWeatherForLocationAsync(location);

            // Read adaptive card template
            var cardTemplate = await File.ReadAllTextAsync(_adaptiveCardFilePath, cancellationToken);

            // Render adaptive card content
            var cardContent = new AdaptiveCardTemplate(cardTemplate).Expand
            (
                new HelloWorldModel
                {
                    Title = "Your Hello World Bot is Running",
                    Body = "Congratulations! Your hello world bot is running. Click the documentation below to learn more about Bots and the Teams Toolkit.",
                }
            );

            // Build attachment
            var activity = MessageFactory.Attachment
            (
                new Attachment
                {
                    ContentType = "application/vnd.microsoft.card.adaptive",
                    Content = JsonConvert.DeserializeObject(cardContent),
                }
            );

            // send response
            return new ActivityCommandResponse(activity);
        }
    }
}
