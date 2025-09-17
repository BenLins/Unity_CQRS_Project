#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using Project.Domain.Entities;
using Project.Application.Abstractions.Stores;
using Project.Infrastructure.Stores.Models;

namespace Project.Infrastructure.Stores
{
    public class PlayerPrefsCountersStore : ICountersStore, IDisposable
    {
        private const string CounterListKey = "CounterList";
        
        private readonly Dictionary<string, CounterDataContainer> _countersCache = new();
        
        public PlayerPrefsCountersStore()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            if (!PlayerPrefs.HasKey(CounterListKey))
            {
                return;
            }
            
            var counterListJson = PlayerPrefs.GetString(CounterListKey);

            var counterList = JsonConvert.DeserializeObject<CountersList>(counterListJson);
                
            if (counterList.Ids is null)
            {
                return;
            }
            
            foreach (var counterId in counterList.Ids)
            {
                var counterDataContainerJson = PlayerPrefs.GetString(counterId);

                if (string.IsNullOrWhiteSpace(counterDataContainerJson))
                {
                    PlayerPrefs.DeleteKey(counterId);
                    
                    continue;
                }
                
                var counterDataContainer = JsonConvert.DeserializeObject<CounterDataContainer>(counterDataContainerJson);
                
                _countersCache.Add(counterId, counterDataContainer);
            }
        }
        
        public void Dispose()
        {
            var counterList = new CountersList
            {
                Ids = _countersCache.Keys.ToArray()
            };
            
            PlayerPrefs.SetString(CounterListKey, JsonConvert.SerializeObject(counterList));
            
            foreach (var counterDataContainerPair in _countersCache)
            {
                PlayerPrefs.SetString(
                    counterDataContainerPair.Key, 
                    JsonConvert.SerializeObject(counterDataContainerPair.Value));
            }
        }
            
        public Task<Counter[]> GetAll(CancellationToken cancellationToken)
        {
            var counters = _countersCache
                .Select(item => MapToDomain(item.Value))
                .ToArray();
            
            return Task.FromResult(counters);
        }

        public Task Save(Counter counter, CancellationToken cancellationToken)
        {
            _countersCache[counter.Id] = MapToDataContainer(counter);
            
            return Task.CompletedTask;
        }

        public Task<Counter?> GetById(String id, CancellationToken cancellationToken)
        {
            if (!_countersCache.TryGetValue(id, out var counterDataContainer))
            {
                return Task.FromResult<Counter?>(null);
            }

            var counter = MapToDomain(counterDataContainer);
            
            return Task.FromResult(counter)!;
        }
        
        private static Counter MapToDomain(CounterDataContainer counterDataContainer)
        {
            return new Counter(counterDataContainer.Id, counterDataContainer.Name, counterDataContainer.Value, counterDataContainer.Step);
        }

        private static CounterDataContainer MapToDataContainer(Counter counter)
        {
            return new CounterDataContainer
            {
                Id = counter.Id,
                Name = counter.Name,
                Value = counter.Value,
                Step = counter.Step
            };
        }
    }
}
