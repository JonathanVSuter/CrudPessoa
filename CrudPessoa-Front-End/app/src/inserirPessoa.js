async function Salvar(){
    let nome = document.querySelector('#nome').value;      
    let cpf = document.querySelector('#cpf').value;      
    let rg = document.querySelector('#rg').value;      
    let dataDeNascimento = document.querySelector('#dataNascimento').value;  
        
    let pessoa = {
        nome,
        cpf,
        rg,
        dataDeNascimento
    };

    let salvarPessoaViewModel = {
        pessoa        
    };

    let response = await EnviarApi(salvarPessoaViewModel);
    console.log(response);
}
    
    //função para fazer uma request na api;
async function EnviarApi(viewmodel){
    
    //opções/dados para fazer a request;
    const options = {
    //método, se é um post, get etc..
        method: 'POST', 
        headers:{'content-type': 'application/json'},       
        body: JSON.stringify(viewmodel) 
    };

    //TODO: mudar a url para o seu localhost.
    const req =  await fetch('https://localhost:44365/pessoa/salvar', options )
    //caso a request dê certo, retornará a resposta;
    .then(response => {      
        response.text()
        .then(data=>  {
            alert(data);
            return data;
        });
    }) 
    //caso dê erro, irá retornar o erro e mostrar no console
    .catch(erro => {
        console.log(erro);
        return erro;
    });

    return req;
}

function Voltar(){
    window.history.back();
}