﻿using NeoCortexApi.Entities;
using NeoCortexEntities.NeuroVisualizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace WebSocketNeuroVisualizer
{

    public class WSNeuroVisualizer : INeuroVisualizer
    {

        readonly string url = "ws://localhost:5000/ws/client1";

        ClientWebSocket websocket = new ClientWebSocket();

        public Task InitModelAsync(NeuroModel model)
        {
            return Task.FromResult<bool>(true);
           // model = new NeuroModel
           // {
           //     MsgType = "init"

           // };

           //await SendData(websocket,  model.ToString(), true);
        }

        public async Task UpdateColumnOverlapsAsync(List<MiniColumn> columns)
        {
            MiniColumn obj= null;
            for (int i = 0; i < columns.Count; i++)
            {
                 obj = new MiniColumn
                {
                    Overlap = columns[i].Overlap,
                    ColDims = columns[i].ColDims,
                    MsgType = columns[i].MsgType

                };

            }
            await SendData(websocket, obj.ToString(), true);
        }
        public async Task UpdateSynapsesAsync(List<SynapseData> synapses)
        {

            SynapseData updateSynapses = null;
            Cell postSynapCell = null;
            for (int syn = 0; syn < synapses.Count; syn++)
            {
                if (synapses[syn].Synapse.Segment is DistalDendrite)
                {
                    DistalDendrite seg = (DistalDendrite)synapses[syn].Synapse.Segment;
                    postSynapCell = seg.ParentCell;
                }
                else if (synapses[syn].Synapse.Segment is ProximalDendrite)
                {
                    ProximalDendrite seg = (ProximalDendrite)synapses[syn].Synapse.Segment;
                    
                    // DimX = seg.ParentColumnIndex
                    // DImZ = 4;
                }
                updateSynapses = new SynapseData
                {
                    PreCell = synapses[syn].Synapse.SourceCell,
                    PostCell = postSynapCell,
                    MsgType = synapses[syn].MsgType
                };


            }
 
            await SendData(websocket, updateSynapses.ToString(), true);

        }
        public async Task Connect(string url, CancellationToken cancellationToken)
        {
            try
            {
                // once per Minute H:M:S
                websocket.Options.KeepAliveInterval = new TimeSpan(0, 1, 0);
                await websocket.ConnectAsync((new Uri(url)), CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            }


        }

        public async Task SendData(ClientWebSocket websocket, string message, bool endOfMessage)
        {

            if (websocket != null && websocket.State == WebSocketState.Open)
            {
                var msg = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                await websocket.SendAsync(new ArraySegment<byte>(msg), WebSocketMessageType.Text, endOfMessage, CancellationToken.None);

            }

        }

    }

}
