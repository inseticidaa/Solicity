using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicity.Domain.Enums
{
    public enum IssueStatusEnum
    {
        Open = 1, // Aberto para discussao

        Cancelled, // Projeto cancelado
        Merged, // Merged/ Duplicidade

        Planning, // Discussao fechada, Desenvolvimento daas historias

        InReview, // Tudo pronto e documentado... Aprovacao com base no topico

        Rejected, // Nao passou na review -> Cancelar ou Abrir
        Approved, // Aprovado, adicionar cartoes no backlog das equipes 

        Done, // Assim que todas as tarefas estiverem prontas norificar usuarios da conclusao da issue
    }
}
