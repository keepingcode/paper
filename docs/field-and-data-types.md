FieldTypes:

-   hidden
-   submit
-   text
-   tel
-   search
-   url
-   email
-   password
-   color
-   number
-   range
-   date
-   time
-   datetime
-   month
-   week
-   checkbox
-   file
-   __select

DataTypes:

-   binary
-   boolean
-   date
-   dateTime
-   datetime
-   decimal
-   integer
-   string
-   time
-   record

Correspondências:

FieldType       DataType
--------------  --------
hidden          string
hidden          integer
hidden          decimal
hidden          dateTime
hidden          date
hidden          time
hidden          boolean
submit          (null)
submit          string
submit          integer
submit          decimal
submit          date
submit          time
submit          datetime
submit          boolean
text            string
tel             string
search          string
url             string
email           string
password        string
color           string
number          integer
number          decimal
range           integer
date            date
date            datetime
time            time
time            datetime
datetime        datetime
datetimeLocal   datetime
month           date
month           datetime
week            date
week            datetime
checkbox        boolean
file            binary

Caixa de Seleção (select)

-   FieldType deve ser "text".
-   Qualquer DataType é suportado.
-   A propriedade "Value" do Field deve conter as opções da caixa de seleção.

Seletor de Registros

-   Caixa de seleção para seleção de um ou mais objetos Entity da classe "record"
-   FieldType deve ser ?
-   DataType deve ser "record"
