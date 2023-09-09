# WPFTabs

## Descrizione
#### Esercizio WPF in C# da 5 tab diverse (Bubble, Collatz, Telefono, Acronimo, Isogramma). Ogni tab avrà una sua funzione che verrà eseguita indipendentemente (anche a tab non aperta) e disporrà inoltre di una descrizione della propria funzione e di un footer contenente data e ora.


## Griglia
Per prima cosa bisognerà cerare una griglia che andrà a contenere tab e footer, di modo da non dover creare un footer identico per ogni tab e poter riutilizzare lo stess, sempliccemente posizionandolo in basso

```xaml
<Grid>
        <Grid.RowDefinitions>
            <RowDefinition></RowDefinition>
            <RowDefinition Height="50"></RowDefinition>
        </Grid.RowDefinitions>
```

Il primo tag <b><i><Grid></i></b> andrà chiuso una volta sviluppata la partre grafica anche delle tab, dato che sarà la griglia che conterrà l'intera applicazione. Delle due righe all'interno di <b><i><Grid.RowDefinitions></i></b> invece, la prima andrà a contenere le diverse tab e la seconda il footer, proprio per questo essa verrà ridimensionata, di modo da non far occupare troppo spazio al nostro footer. 



## Footer
Il primo componente da creare sarà il footer, comune per tutte le diverse tab; come prima cosa guarderemo il lato estetico.

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
Per prima cosa abbiamo creato 
