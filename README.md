### Sistema Hamburgueri (.NET 8 / Blazor Server)
## Equipe de Desenvolvimento

- **Lucas Soares Francisco**: 06011048

### Diagrama de Classes do Sistema

---

### Objetivo do Sistema

O objetivo deste projeto é simular uma hamburgueria online, permitindo:

Visualizar produtos por categoria

Adicionar produtos ao carrinho

Finalizar um pedido

Calcular frete

Simular pagamento

Registrar o pedido para um usuário logado

Exibir informações na interface (Blazor Server)

O foco é atender aos critérios da AV2: modelagem orientada a objetos, polimorfismo, DTOs, exceções, serviços e documentação.

---

### 1. Classes do Domínio
## 1.1 Category

Representa uma categoria de produto.

## Propriedades:

Id

Name

## Função:

Organiza produtos em grupos (Entrada, Prato, Sobremesa).

## 1.2 Product

Produto do cardápio.

## Propriedades:

Id

Name

Price

Description

SpecialTag

ImageUrl

CategoryId

Função:

Exibido na interface e usado no carrinho e pedidos.

## 1.3 ShoppingCart

Itens adicionados ao carrinho por um usuário.

## Propriedades:

Id

UserId

ProductId

Count

## Função:

Armazena quantidade de cada produto que o usuário deseja comprar.

## 1.4 ApplicationUser

Usuário do sistema (herda de IdentityUser).

## Propriedades herdadas:

Id

UserName

Email

PhoneNumber

Função:

Autenticação e vinculação dos pedidos.

## 1.5 OrderHeader

Informações gerais do pedido.

## Propriedades:

Id

UserId

OrderTotal

Shipping

OrderDate

PaymentMethod

Status

Name

PhoneNumber

Email

## Função:

Cabeçalho do pedido finalizado.

## 1.6 OrderDetail

Itens individuais do pedido.

## Propriedades:

Id

OrderId

ProductId

Count

Price

## Função:

Lista todos os produtos comprados em cada pedido.

---

### 2. Serviços e Polimorfismo
## 2.1 Interface IPaymentService

Define o comportamento das formas de pagamento.

## Métodos:

Metodo

ValidarPagamento(valor)

ProcessarPagamento(valor)

## 2.2 PagamentoPixService

Implementa pagamento via PIX.

## Função:

Valida o valor

Gera um token ilustrativo (ex: PIX-TOKEN-XXXX)

## Simulado — não realiza pagamentos reais.

## 2.3 PagamentoCartaoService

Implementa pagamento via cartão.

## Função:

Valida o valor

Gera um código de autorização fictício

## Também totalmente ilustrativo.

---

### 3. Cálculo de Frete (Strategy Pattern)
## 3.1 Interface ICalculadoraFrete

Define o cálculo de frete.

## Métodos:

Nome

Calcular(subtotal)

## 3.2 FretePadrao
Regras:

Frete grátis para subtotal ≥ 150

Caso contrário: R$ 12,90

## 3.3 FreteExpresso
Regras:

Frete fixo: R$ 19,90

---

### 4. DTOs
## 4.1 ProductDTO

Id

Nome

Preco

ImagemUrl

## 4.2 ShoppingCartDTO

ProductId

Quantidade

## 4.3 OrderDetailDTO

ProductId

Quantidade

PrecoUnitario

## 4.4 OrderDTO

Id

UserId

Subtotal

Frete

Total

MetodoPagamento

---

### 5. Repositórios

Repositórios responsáveis por acessar o banco via Entity Framework:

## Implementações:

CategoryRepository

ProductRepository

ShoppingCartRepository

OrderRepository

Cada um contém operações:

Criar

Atualizar

Remover

Buscar por ID

Buscar todos

Mantém o código organizado e com baixo acoplamento.

---

### 6. Persistência (ApplicationDbContext)

O ApplicationDbContext mapeia todas as entidades e contém Seed inicial:

Categorias

Produtos

Também herda de IdentityDbContext para incluir tabela de usuários.

---

### 7. Regras de Negócio
Carrinho:

Adicionar produtos

Aumentar/diminuir quantidade

Remover item quando quantidade chega a 0

## Pedido:

Criar cabeçalho (OrderHeader)

Criar detalhes (OrderDetail)

Calcular subtotal, frete e total

## Pagamento:

Métodos ilustrativos

Estruturado com polimorfismo

## Frete:

Padrão ou expresso via Strategy

---

### 8. Exceções (DomainException)

Usada para validar regras como:

Valor de pagamento inválido

Quantidade negativa

Tentativa de finalizar pedido vazio

Fornece mensagens claras sem quebrar a aplicação.

---

### 9. Fluxo do Sistema

Usuário acessa a página inicial

Visualiza os produtos

Adiciona ao carrinho

Abre o carrinho e revisa itens

Escolhe frete e pagamento

Finaliza o pedido

Sistema registra o pedido no banco

Exibe confirmação

---

### 10. Pagamento — IMPORTANTE

O sistema de pagamento é 100% ilustrativo, feito apenas para demonstrar polimorfismo, conforme solicitado na AV2.

Não envia PIX

Não cobra cartão

Não faz requisições externas

Não envolve dados reais

É apenas uma simulação controlada para fins acadêmicos.

---

### Conclusão

## O projeto segue todos os critérios da AV2:

✔ Modelagem coerente
✔ Polimorfismo aplicado (pagamento + frete)
✔ DTOs
✔ Exceções
✔ Repositórios
✔ Documentação clara
✔ UML completo
