using Bonfiglioli4p0.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonfiglioli4p0.ViewModel.Services
{
    public static class SequencingService
    {
        /// <summary>
        /// Resetto l'ordine sequenziale della observ
        /// </summary>
        public static ObservableCollection<T> SetCollectionSequence<T>(ObservableCollection<T> targetCollection) where T : ISequencedObject
        {
            var sequenceNumber = 1;
            // Risequenza
            foreach (ISequencedObject sequencedObject in targetCollection)
            {
                sequencedObject.SequenceNumber = sequenceNumber;
                sequenceNumber++;
            }

            return targetCollection;
        }
    }
}
