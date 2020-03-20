using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FolkerKinzel.Contacts.IO.Resources;

namespace FolkerKinzel.Contacts.IO
{
    /// <summary>
    /// Eine Liste, die mit den in ihr enthaltenen <see cref="Tuple{T1, T2}">Tuple&lt;string, ContactProperty&gt;</see>-Objekten die Reihenfolge der Spaltennamen
    /// einer CSV-Datei (<see cref="Tuple{T1, T2}.Item1"/>) und die Zuordnung dieser Spaltenamen
    /// zu Eigenschaften der <see cref="Contact"/>-Klasse (<see cref="Tuple{T1, T2}.Item2"/>) beschreibt. 
    /// </summary>
    /// <remarks>
    /// <para>
    /// Auf die Einträge der Liste kann über den Index und über
    /// die mit <see cref="Tuple{T1, T2}.Item2"/> referenzierte Eigenschaft der <see cref="Contact"/>-Klasse zugegriffen werden. Die Werte von
    /// <see cref="Tuple{T1, T2}.Item2"/> müssen daher eindeutig sein.
    /// </para>
    /// <para>
    /// Die Collection kann keine <c>null</c>-Werte enthalten.
    /// </para>
    /// <para>
    /// Beim Versuch, ein <see cref="Tuple{T1, T2}"/> einzufügen, bei dem der mit <see cref="Tuple{T1, T2}.Item1"/> angegebene
    /// CSV-Spaltenname <c>null</c> ist oder <see cref="string.Empty"/> oder nur aus Leerraum besteht, wird eine <see cref="ArgumentException"/> geworfen.
    /// </para>
    /// </remarks>
    public class CsvMappingCollection : KeyedCollection<ContactProperty, Tuple<string, ContactProperty>>
    {
        /// <summary>
        /// Extrahiert den Schlüssel aus <paramref name="item"/>.
        /// </summary>
        /// <param name="item">Das Element, aus dem der Schlüssel extrahiert werden soll.</param>
        /// <returns>Der Schlüssel für das angegebene Element.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> ist <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Der mit <see cref="Tuple{T1, T2}.Item1"/> angegebene
        /// CSV-Spaltenname ist <c>null</c> oder <see cref="string.Empty"/> oder besteht nur aus Leerraum.</exception>
        protected override ContactProperty GetKeyForItem(Tuple<string, ContactProperty> item)
        {
            ContactProperty key = item?.Item2 ?? throw new ArgumentNullException(nameof(item));

            if(string.IsNullOrWhiteSpace(item.Item1))
            {
                throw new ArgumentException(Res.ColumnNameNotNull);
            }

            return key;
        }
    }
}
