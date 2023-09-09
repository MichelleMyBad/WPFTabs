# WPFTabs

## Descrizione

###Esercizio WPF in C# da 5 tab diverse (Bubble, Collatz, Telefono, Acronimo, Isogramma). Ogni tab avrà una sua funzione che verrà eseguita indipendentemente (anche a tab non aperta) e disporrà inoltre di una descrizione della propria funzione e di un footer contenente data e ora.


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
