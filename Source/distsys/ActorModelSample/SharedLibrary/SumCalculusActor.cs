using AkkaSb.Net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary
{
    public class SumCalculusActor : ActorBase
    {
        public SumCalculusActor(ActorId id) : base(id)
        {
            Receive<string>((str) =>
            {
                return null;
            });

            Receive<SumMsg>(((msg) =>
            {
                this.Logger?.LogInformation($"Received message: '{msg.GetType().Name}', sum of ({msg.SumStart}-{msg.SumEnd})");

                double sum = 0;

                for (long i = msg.SumStart; i < msg.SumEnd; i++)
                {
                    sum += i;
                }

                this.Logger?.LogInformation($"Completed sum = {sum}");

                return sum;
            }));

            Receive<long>((long num) =>
            {
                return num + 1;
            });

            Receive<DateTime>((DateTime dt) =>
            {
                return dt.AddDays(1);
            });
        }

     
        public override void Activated()
        {
            this?.Logger.LogTrace("I'm activated.");
        }
    }

}
