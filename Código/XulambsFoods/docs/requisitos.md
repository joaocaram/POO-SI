﻿# Xulambs ~~Pizza~~ Foods

## Versão 0.1 - Vendendo Pizzas

Xulambs Pizza é uma pizzaria que será inaugurada em breve, com grandes expectativas. Inicialmente, o negócio precisa automatizar o cálculo do preço de venda das pizzas. O modelo de vendas segue uma lógica simplificada:

1. Não existem pizzas de sabores pré-definidos.
1. A pizza tem preço inicial de `R$29`.
1. A pizza pode ter até `8` ingredientes adicionais.
1. Os adicionais têm custo fixo: cada um custa `R$5`.

O **Sistema Xulambs Pizza** precisa permitir registrar vendas de pizzas isoladas e emitir um cupom de venda (relatório) para cada uma, contendo sua descrição e valores a serem pagos.

## Versão 0.2 - A pizzaria e seus pedidos

A pizzaria percebeu que é melhor agrupar as vendas de pizzas em pedidos. Foram levantados os requisitos:

1. Um pedido deve ter um identificador único.
1. Um pedido deve ter sua data armazenada.
1. Um pedido aceitará novos itens até que seja fechado.
1. O relatório de um pedido deve mostrar a descrição de cada uma das pizzas, detalhadamente, e o valor total do pedido.

## Versão 0.3 - Pedidos para entrega

A pizzaria quer ampliar seus negócios realizando _pedidos para entrega_ de pizzas.

  - Um **pedido local** continua com as regras atuais, porém passando a cobrar uma taxa de `10%`  de serviço para os atendentes.
  - Um **pedido para entrega** tem um limite de `6` pizzas. Ele não tem taxa de serviço, mas sim taxa de entrega de acordo com a distância:
    - Até `4km`: isento.
    - De `4,01` até `8km`: `R$5`
    - Acima de `8km`: `R$8`

## Versão 0.3b - Sanduíches e Pizzas

A Xulambs Pizza está progredindo rapidamente nos seus negócios locais e para entrega. Agora, querem diversificar seu público oferecendo outras opções de comidas. O primeiro passo nessa direção é a inclusão de _sanduíches_ no cardápio. 

Os sanduíches seguem um modelo de venda similar ao que está dando certo para as pizzas:
  - Preço inicial de `R$15`;
  - Podem receber até `5` adicionais, custando `R$3` cada;
  - Podem fazer parte de um _combo com fritas_ e, neste caso, há um acréscimo de `R$5`.

Para incentivar a venda de pizzas, que são mais caras, os gestores decidiram criar uma nova regra de preço: a partir do `6° adicional` da pizza, eles terão `50%` de desconto.
