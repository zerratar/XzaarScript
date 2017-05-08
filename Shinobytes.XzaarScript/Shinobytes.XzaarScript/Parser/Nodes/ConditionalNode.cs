using Shinobytes.XzaarScript.Ast;

namespace Shinobytes.XzaarScript.Parser.Nodes
{
    public class ConditionalNode : AstNode
    {
        private readonly AstNode condition;
        private AstNode ifTrue;
        private AstNode ifFalse;

        public ConditionalNode(AstNode condition, AstNode ifTrue, AstNode ifFalse, int nodeIndex)
            : base(SyntaxKind.Block, "CONDITIONAL", null, nodeIndex)
        {
            if (condition != null) condition.Parent = this;
            if (ifFalse != null) ifFalse.Parent = this;
            if (ifTrue != null) ifTrue.Parent = this;


            this.condition = condition;
            this.ifTrue = ifTrue;
            this.ifFalse = ifFalse ?? Empty();

        }

        public AstNode GetCondition() => condition;

        public AstNode GetTrue() => ifTrue;

        public AstNode GetFalse() => ifFalse;

        public override bool IsEmpty()
        {
            return GetCondition() == null && GetTrue() == null && GetFalse() == null;
        }

        public override string ToString()
        {
            var conditionString = condition.ToString();
            if (condition.Kind != SyntaxKind.Expression)
                conditionString = "(" + conditionString + ")";
            return $"if {conditionString} {{{ifTrue}}} else {{{ifFalse}}}";
        }

        public override void Accept(INodeVisitor nodeVisitor)
        {
            nodeVisitor.Visit(this);
        }

        public void SetIfTrue(AstNode node)
        {
            ifTrue = node;
        }

        public void SetIfFalse(AstNode node)
        {
            ifFalse = node;
        }
    }
}