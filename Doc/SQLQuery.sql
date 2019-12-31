USE BD_VENDAS_FITEX
/*FISICOS*/
INSERT INTO FISICOS
VALUES( GETDATE() , 'lazaro.duarte@gmail.com', '1532353698','15965256987','15698158','Rua Oswaldo Cruz', '241', 'Centro', '-', 'São Paulo', 'SP', 'Ativo', 'Lazaro Duarte', '54123696518','21/08/1994','425698547');

/*JURIDICOS*/
INSERT INTO JURIDICOS
VALUES( GETDATE()  , 'letrinhas@companhia.com', '1532316999','1532321588','13045189','Av. Brasil', '555', 'Benfica', 'PREDIO 2', 'Rio de Janeiro', 'RJ', 'Ativo', 'companhiadasletrinhas', '56654589548','Marlene Dantas','1254569854');

/*PRODUTOS*/
INSERT INTO PRODUTOS
VALUES('FITA GOMADA EB20', 'AZUL', 'UN', 5.20, 500, 100, 0.02, 2.1, 'Ativo', 0, 0)
INSERT INTO PRODUTOS
VALUES('FITA GOMADA EB50', 'AMARELA', 'UN', 5.20, 480, 100, 0.02, 2.1, 'Ativo', 0, 0)
INSERT INTO PRODUTOS
VALUES('FITA POLYPRESS 470', 'BEGE', 'CX', 68.9, 250, 80, 0.05, 6.3, 'Ativo', 0, 0)
INSERT INTO PRODUTOS
VALUES('FITA POLYPACK 470', 'BEGE', 'MT', 25.9, 380, 80, 0.03, 3.4, 'Ativo', 0, 0)
INSERT INTO PRODUTOS
VALUES('FITA POLYPACK 380 TR', 'AZUL', 'MT', 28.9, 380, 80, 0.03, 3.4, 'Ativo', 0, 0)

/*USUARIOS*/
INSERT INTO USUARIOS
VALUES('', 'Administrador',	'', 'Administrador', 'ad1320', 'Gerente' )
INSERT INTO USUARIOS
VALUES('32659854518', 'Fernando Macedo',	'fernando.macedo@gmail.com', 'macedo', '456987', 'Gerente' )
INSERT INTO USUARIOS
VALUES('37854698515', 'Aline Prado',	'alinefernanda.prado@gmail.com', 'aprado', '985658', 'Vendedor' )

/*PEDIDOS*/
INSERT INTO PEDIDOS
VALUES (GETDATE(),'Varejo','5101','Retirada','Vista','Sem Frete',0.00,1,26.00,26.00,'','Importado','Fisico',1,1,'S',NULL)
/*ITEM*/
INSERT INTO ItemPedidos
VALUES(1,5.20,8,41.60,1)
INSERT INTO ItemPedidos
VALUES(3,68.90,2,137.8,1)


