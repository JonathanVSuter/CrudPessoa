async function PreencherTabelaPessoas(resposta, limpar){
    
    let tabela = document.querySelector('#listagem-pessoas');    

    if(limpar)
        tabela.innerHTML = '';

    if(!resposta.sucesso)
        alert(resposta.mensagem);
    else{
        resposta.resultado.forEach(function(e) {
            let linha = document.createElement('tr');
            linha.addEventListener('click', ()=> {            
                window.location.href = "./alterarPessoa.html?id=" + e.id;
            });
            
            let idInput = document.createElement('input');
            idInput.type = 'hidden';
            let nomeTd = document.createElement('td');
            nomeTd.classList.add('row-nome-pessoa');
            let cpfTd = document.createElement('td');
            cpfTd.classList.add('row-cpf-pessoa');
            let rgTd = document.createElement('td');
            rgTd.classList.add('row-rg-pessoa');
            let dataNascimentoTd = document.createElement('td');
            dataNascimentoTd.classList.add('row-data-nascimento-pessoa');
            let dataAtualizacaoTd = document.createElement('td');
            dataAtualizacaoTd.classList.add('row-data-atualizacao-pessoa');
            let dataRegistroTd = document.createElement('td');
            dataRegistroTd.classList.add('row-data-registro-pessoa');
                        
            idInput.value = e.id;
            nomeTd.innerHTML = e.nome;
            cpfTd.innerHTML = e.cpf;
            rgTd.innerHTML = e.rg;
            dataNascimentoTd.innerHTML = e.dataDeNascimento;            
            dataAtualizacaoTd.innerHTML = e.dataDeAtualizacao;
            dataRegistroTd.innerHTML = e.dataDeRegistro;
    
            linha.appendChild(idInput);
            linha.appendChild(nomeTd);
            linha.appendChild(cpfTd);
            linha.appendChild(rgTd);            
            linha.appendChild(dataNascimentoTd);            
            linha.appendChild(dataAtualizacaoTd);            
            linha.appendChild(dataRegistroTd);            
            
            tabela.appendChild(linha);
        });
    }
}
async function ListarPessoas(){  
    
    const options = {
        method: 'GET',  
        headers:{'content-type': 'application/json'}                     
    };    
    const req =  await fetch('https://localhost:44365/pessoa/listartodas', options )
        .then(response => {                
            return response.json();
        })     
        .catch(erro => {
            console.log(erro);
            return erro;
        });
    return req;
}
function Voltar(){
    window.history.back();
}
async function ListarPorCriterio(elemento){
    let texto = elemento.value;
    let resposta = await ListarPessoasUsandoCriterio(texto);
    PreencherTabelaPessoas(resposta, true);
}
async function ListarPessoasUsandoCriterio(criterio){  
    
    const options = {
        method: 'GET',  
        headers:{'content-type': 'application/json'}                     
    };    
    const req =  await fetch('https://localhost:44365/pessoa/Listarporcriterio?criterio='+criterio, options )
        .then(response => {              
            return response.json();
        })     
        .catch(erro => {
            console.log(erro);
            return erro;
        });
    return req;
}
//inicia a listagem.
(async() => {
    let res = await ListarPessoas();
    PreencherTabelaPessoas(res, false);    
})();