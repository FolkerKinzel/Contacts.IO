using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FolkerKinzel.Contacts.IO.Resources;

namespace FolkerKinzel.Contacts.IO
{
    /// <summary>
    /// Eine Liste, die mit den in ihr enthaltenen <see cref="Tuple{T1, T2}">Tuple&lt;string, ContactProperty?&gt;</see>-Objekten die Reihenfolge der Spaltennamen
    /// einer CSV-Datei (<see cref="Tuple{T1, T2}.Item1"/>) und die Zuordnung dieser Spaltenamen
    /// zu Eigenschaften der <see cref="Contact"/>-Klasse (<see cref="Tuple{T1, T2}.Item2"/>) beschreibt. 
    /// </summary>
    /// <remarks>
    /// <para>
    /// Auf die Einträge der Liste kann über den Index und über
    /// den mit <see cref="Tuple{T1, T2}.Item1"/> referenzierten Spaltenname der CSV-Datei zugegriffen werden. Die Werte von
    /// <see cref="Tuple{T1, T2}.Item1"/> müssen daher eindeutig und nicht <c>null</c> sein.
    /// </para>
    /// <para>
    /// Die Collection kann keine <c>null</c>-Werte enthalten.
    /// </para>
    /// <para>
    /// Beim Versuch, ein <see cref="Tuple{T1, T2}"/> einzufügen, bei dem der mit <see cref="Tuple{T1, T2}.Item1"/> angegebene
    /// CSV-Spaltenname <c>null</c> ist, wird eine <see cref="ArgumentNullException"/> geworfen.
    /// </para>
    /// </remarks>
    internal class CsvMappingCollection : KeyedCollection<string, Tuple<string, ContactProp?>>
    {
        /// <summary>
        /// Extrahiert den Schlüssel aus <paramref name="item"/>.
        /// </summary>
        /// <param name="item">Das Element, aus dem der Schlüssel extrahiert werden soll.</param>
        /// <returns>Der Schlüssel für das angegebene Element.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> oder <paramref name="item"/>.Item1 ist <c>null</c>.</exception>
        protected override string GetKeyForItem(Tuple<string, ContactProp?> item)
        {
            return item?.Item1 ?? throw new ArgumentNullException(nameof(item));
        }
    }
}
