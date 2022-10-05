async function getPessoaId(){
    const urlParams = new URLSearchParams(window.location.search);    
    let res = await BuscarPorId(urlParams.get('id'));
    PreencherFormulario(res);
}
async function BuscarPorId(id){      
    const options = {
        method: 'GET',  
        headers:{'content-type': 'application/json'}                     
    };    
    const req =  await fetch('https://localhost:44365/pessoa/encontrarporid?id='+id, options )
        .then(response => {      
            return response.json();
        })     
        .catch(erro => {
            console.log(erro);
            return erro;
        });
    return req;
}
async function PreencherFormulario(json){
    
    let dadosForm = document.querySelector('#form');
    let id = dadosForm.querySelector('#id-pessoa');    
    let nome = dadosForm.querySelector('#nome');
    let cpf = dadosForm.querySelector('#cpf');
    let rg = dadosForm.querySelector('#rg');
    let dataNascimento = dadosForm.querySelector('#dataNascimento');    

    id.value = json.resultado.id;
    nome.value = json.resultado.nome;
    cpf.value = json.resultado.cpf;
    rg.value = json.resultado.rg;    
    dataNascimento.valueAsDate = new Date(json.resultado.dataDeNascimento);
}
async function EnviarApi(viewmodel){
        
    const options = {    
        method: 'PUT', 
            headers:{'content-type': 'application/json'},       
            body: JSON.stringify(viewmodel) 
    };

    //TODO: mudar a url para o seu localhost.
    const req =  await fetch('https://localhost:44365/pessoa/alterar', options )
    //caso a request dê certo, retornará a resposta;
    .then(response => {      
        response.text()
        .then(data=>  {            
            return data;
        });
    })     
    .catch(erro => {
        console.log(erro);
        return erro;
    });

    return req;
}
async function Atualizar(){
    let id = parseInt(document.querySelector('#id-pessoa').value);    
    console.log(id);
    let nome = document.querySelector('#nome').value;  
    console.log(nome);
    let cpf = document.querySelector('#cpf').value;  
    console.log(cpf);
    let rg = document.querySelector('#rg').value;  
    console.log(cpf);    
    let dataDeNascimento = document.querySelector('#dataNascimento').value;  
    console.log(dataDeNascimento);
    
    let pessoa = {
        id,
        nome,
        cpf,
        rg,
        dataDeNascimento        
    };

    let atualizarPessoaViewModel = {
        pessoa        
    };

    console.log(atualizarPessoaViewModel);

    let response = await EnviarApi(atualizarPessoaViewModel);
    console.log(response);
}
function Voltar(){
    window.history.back();
}

getPessoaId();