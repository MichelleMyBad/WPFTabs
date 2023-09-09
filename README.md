# WPFTabs

## Descrizione
#### Esercizio WPF in C# da 5 tab diverse (Bubble, Collatz, Telefono, Acronimo, Isogramma). Ogni tab avrà una sua funzione che verrà eseguita indipendentemente (anche a tab non aperta) e disporrà inoltre di una descrizione della propria funzione e di un footer contenente data e ora.


## Griglia
Per prima cosa bisognerà cerare una griglia che andrà a contenere tab e footer, di modo da non dover creare un footer identico per ogni tab e poter riutilizzare sempre lo stesso sempliccemente posizionandolo in basso.
        
```xaml
<Grid>
        <Grid.RowDefinitions>
            <RowDefinition></RowDefinition>
            <RowDefinition Height="50"></RowDefinition>
        </Grid.RowDefinitions>
```

Il primo tag <b><i><</i></b><b><i>Grid</i></b><b><i>></i></b> andrà chiuso una volta sviluppata la partre grafica anche delle tab, dato che sarà la griglia che conterrà l'intera applicazione. Delle due righe all'interno di <b><i><Grid.RowDefinitions></i></b> invece, la prima andrà a contenere le diverse tab e la seconda il footer, proprio per questo essa verrà ridimensionata, di modo da non far occupare troppo spazio al nostro footer. 



## Footer
Dopo aver creato la prima griglia, il prossimo componente da creare sarà il footer, comune per tutte le diverse tab; come prima cosa ne osserveremo la struttura grafica.

<details>
<summary>xaml</summary>

```xaml
<Grid Grid.Row="1" Height="50" VerticalAlignment="Bottom">
    <Grid.RowDefinitions>
        <RowDefinition></RowDefinition>
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
        <ColumnDefinition></ColumnDefinition>
        <ColumnDefinition></ColumnDefinition>
        <ColumnDefinition></ColumnDefinition>
        <ColumnDefinition></ColumnDefinition>
    </Grid.ColumnDefinitions>

    <StackPanel Grid.Column="3" VerticalAlignment="Center" HorizontalAlignment="Center" >
        <TextBlock x:Name="txtDataCo" Text="Data" Grid.Column="2"
                HorizontalAlignment="Center" VerticalAlignment="Center"
                    FontWeight="Bold"
                ></TextBlock>
        <TextBlock x:Name="txtOraCo" Text="Ora" Grid.Column="2"
                HorizontalAlignment="Center" VerticalAlignment="Center"
                ></TextBlock>
    </StackPanel>
</Grid>
```
Anche per il footer stesso sarà nececssario creare una grigla, di modo da poter posizionare data e ora in basso a destra. Per fare ciò definiamo una singola riga e ben 4 colonne, per poi posizionare uno <b><i><StackPanel></i></b> nella quarta colonna (<b><i>Grid.Column="3"</i></b>), ci assicuriamo di allineare a dovere il contenuto che andrà inserito nel nostro <b><i><StackPanel></i></b> tramite <b><i>VerticalAlignment="Center"</i></b> e <b><i>HorizontalAlignment="Center"</i></b>. Successivamente inseriamo all'interno dello <b><i><StackPanel></i></b> 2 <b><i><TextBlock></i></b> all'interno dei quali andremo ad inserire data e ora, che verranno gestiti e calcolati tramite c# all'interno del nostro file <b>MainWindow.xaml.cs</b>, proprio per questo motivo è necessario assegnare un nome ad entrambi i <b><i><TextBlock></i></b> usando <b><i>x:Name="nomeComponente"</i></b>, di modo da poter essere richiamati e modificati tramite c#. Per concludere assegnamo un testo di default ad tramite <b><i>Text="testoDiDefault"</i></b>.
</details>
