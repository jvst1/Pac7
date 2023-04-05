# Weather Prediction

O objetivo deste projeto é desenvolver um algoritmo de aprendizado de máquina capaz de prever o tempo na cidade de Florianópolis com base em dados históricos. Além disso, uma aplicação web será desenvolvida para permitir que os usuários acessem essas previsões de forma fácil e intuitiva.

# Relevância

Este projeto é relevante para a sociedade, pois a previsão do tempo é uma informação essencial para diversas atividades do cotidiano, como planejar viagens, eventos ao ar livre, atividades esportivas, entre outras. Com a aplicação web que fornece previsões precisas do tempo em Florianópolis, as pessoas podem tomar decisões mais informadas e seguras, evitando riscos e atrasos desnecessários.

Além disso, o uso de algoritmos de aprendizado de máquina pode contribuir para a melhoria da precisão das previsões de tempo, o que é particularmente importante em áreas com condições climáticas variáveis, como é o caso de Florianópolis. Dessa forma, o projeto pode ajudar a aumentar a segurança e a eficiência de diversas atividades em Florianópolis, contribuindo para o bem-estar e a qualidade de vida da população.

# Diagrama Caso de Uso

![Diagrama caso de uso](https://user-images.githubusercontent.com/88805708/230232870-96321ae3-9c68-4962-871d-9cecfa6ba096.jpg)

# Workflow

![Workflow do Produto](https://user-images.githubusercontent.com/88805708/230231100-c450519c-4425-4258-9d30-a8f6b019be88.jpg)

# Diagrama de Arquitetura 

![Diagrama de Arquitetura](https://user-images.githubusercontent.com/88805708/230233867-632a921f-16b4-4b20-a496-409880faef09.jpg)

# Funcionalidades da Aplicação Web
* Página inicial: apresentará a temperatura atual e a previsão do tempo atual para Florianópolis, bem como uma previsão para os próximos dias; também haverá um modal que conterá informações como porcentagem de umidade e velocidade do vento.
* Página de configurações: permitirá que os usuários personalizem as preferências de exibição da previsão do tempo e do estilo da página (por exemplo, unidade de temperatura, cor de fundo, fonte de letra etc.).

# Funcionalidades do Algoritmo de Aprendizado de Máquina
* Coleta de Dados: coletará informações de previsão do tempo históricas de Florianópolis.
* Pré-processamento de Dados: pré-processará os dados coletados para garantir que estejam prontos para a modelagem.
* Modelagem: treinará e avaliará modelos de aprendizado de máquina em relação aos dados históricos.
* Implantação: fará a implantação do modelo treinado para a aplicação web, para que os usuários possam acessar as previsões do tempo em tempo real.

# Tecnologias Utilizadas
* Python: para o desenvolvimento do algoritmo de aprendizado de máquina.
* Framework Vue.js: para o desenvolvimento da aplicação web.
* Bibliotecas de Aprendizado de Máquina: como scikit-learn, pandas e numpy, para o desenvolvimento do algoritmo de aprendizado de máquina.
* API de dados do tempo: para coletar informações em tempo real.
* Banco de dados: para armazenar os dados históricos coletados.

# Dados que serão utilizados no treinamento:
* Precipitação total, horária (mm)
* Pressão atmosférica ao nível da estação, horária (mB)
* Pressão atmosférica máxima na hora anterior (aut) (mB)
* Pressão atmosférica mínima na hora anterior (aut) (mB)
* Radiação global (KJ/m²)
* Temperatura do ar - bulbo seco, horária (°C)
* Temperatura do ponto de orvalho (°C)
* Temperatura máxima na hora anterior (aut) (°C)
* Temperatura mínima na hora anterior (aut) (°C)
* Temperatura orvalho máxima na hora anterior (aut) (°C)
* Temperatura orvalho mínima na hora anterior (aut) (°C)
* Umidade relativa máxima na hora anterior (aut) (%)
* Umidade relativa mínima na hora anterior (aut) (%)
* Umidade relativa do ar, horária (%)
* Vento, direção horária (gr) (° (gr))
* Vento, rajada máxima (m/s)
* Vento, velocidade horária (m/s)

# Critérios escolhidos:
* Relevância do projeto e complexidade do projeto: 10%
* Documentação: 20%
* Código-fonte: 20%
* Estruturas de engenharia: 25%
* Apresentação: 25%

# Equipe
* Eduardo Augusto Ferreira,
* Gustavo Bosco,
* João Vitor de Souza Tomio,
* Wilson Marutti.
