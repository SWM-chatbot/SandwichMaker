// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.9.2

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SWMproject.Bots;
using SWMproject.Dialogs;
using Microsoft.Extensions.Configuration;

namespace SWMproject
{
    public class Startup
    {
        private IConfiguration _configuration;
        public static string CosmosDbEndpoint;
        public static string AuthKey;
        public static string PartitionKey;
        public static string DatabaseId;
        public static string ContainerId_order;
        public static string ContainerId_keyword;
        public static string ContainerId_count;
        public Startup(IConfiguration iconfig)
        {
            _configuration = iconfig;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            // Use partitioned CosmosDB for storage, instead of in-memory storage.
            CosmosDbEndpoint = _configuration.GetValue<string>("CosmosDbEndpoint");
            AuthKey = _configuration.GetValue<string>("CosmosDbAuthKey");
            PartitionKey = _configuration.GetValue<string>("CosmosDbPartitionKey");
            DatabaseId = _configuration.GetValue<string>("CosmosDbDatabaseId");
            ContainerId_order = _configuration.GetValue<string>("CosmosDbContainerId1");
            ContainerId_keyword = _configuration.GetValue<string>("CosmosDbContainerId2");
            ContainerId_count = _configuration.GetValue<string>("CosmosDbContainerId3");

            // Create the Bot Framework Adapter with error handling enabled.
            services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();

            // Create the User state. (Used in this bot's Dialog implementation.)
            services.AddSingleton<UserState>();

            // Create the Conversation state. (Used by the Dialog system itself.)
            services.AddSingleton<ConversationState>();

            services.AddSingleton<LocationDialog>();

            // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
            services.AddTransient<IBot, DialogAndWelcomeBot<LocationDialog>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles()
                .UseStaticFiles()
                .UseWebSockets()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

            // app.UseHttpsRedirection();
        }
    }
}
