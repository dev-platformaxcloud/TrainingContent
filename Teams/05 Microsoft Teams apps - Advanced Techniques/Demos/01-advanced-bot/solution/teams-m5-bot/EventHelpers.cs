﻿/*
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Licensed under the MIT license.
 */
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace teams_m5_bot
{
  public class EventHelpers
  {
    public static bool MemberAddedIsBot(Activity memberAddedActivity)
    {
      return memberAddedActivity.MembersAdded.Any(m => m.Id.Equals(memberAddedActivity.Recipient.Id));
    }

    public static async Task SendOneToOneWelcomeMessage(ConnectorClient client, TeamsChannelData channelData,
                                                        ChannelAccount botAccount, ChannelAccount userAccount,
                                                        string tenantId)
    {
      string welcomeMessage = $"The team {channelData.Team.Name} has CardBot - helping your team understand Cards.";

      // create or get existing chat conversation with user
      var response = client.Conversations.CreateOrGetDirectConversation(botAccount, userAccount, tenantId);

      // Construct the message to post to conversation
      Activity newActivity = new Activity()
      {
        Text = welcomeMessage,
        Type = ActivityTypes.Message,
        Conversation = new ConversationAccount
        {
          Id = response.Id
        },
      };

      // Post the message to chat conversation with user
      await client.Conversations.SendToConversationAsync(newActivity);
    }

  }
}