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

    //opções/dados para fazer a request;
    const options = {
        //método, se é um post, get etc..
            method: 'POST', 
            headers:{'content-type': 'application/json'},       
            body: JSON.stringify(salvarPessoaViewModel) 
        };
    
        //TODO: mudar a url para o seu localhost.
        const req =  await fetch('https://localhost:44365/pessoa/salvar', options )
        //caso a request dê certo, retornará a resposta;
        .then(response => {      
            return response.json();            
        }) 
        //caso dê erro, irá retornar o erro e mostrar no console
        .catch(erro => {
            console.log(erro);
            return erro;
        });
    if(req.sucesso){
        alert(req.mensagem); 
        Voltar();
    }
    else {
        alert(req.mensagem); 
    }
} 
function convertToDate(data){
    var pattern = /^(\d{1,2})\/(\d{1,2})\/(\d{4})$/;
    var arrayDate = data.match(pattern);
    var dt = new Date(arrayDate[3], arrayDate[2] - 1, arrayDate[1]);
    return dt;
}
function Voltar(){
    window.location.href = './index.html';
}