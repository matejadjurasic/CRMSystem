import React from 'react';
import { PieChart, Pie, Cell, Tooltip, Legend } from 'recharts';
import { Project } from '../../lib/types';

interface ProjectGraphProps {
  projects: Project[]; 
}

const ProjectGraph: React.FC<ProjectGraphProps> = ({ projects }) => {
  const data = [
    { name: 'Open', value: projects.filter(p => p.status === 'Open').length },
    { name: 'In Progress', value: projects.filter(p => p.status === 'InProgress').length },
    { name: 'Completed', value: projects.filter(p => p.status === 'Completed').length },
    { name: 'Cancelled', value: projects.filter(p => p.status === 'Cancelled').length },
  ];

  const COLORS = ['#0088FE', '#FFBB28', '#00C49F', '#FF8042'];

  return (
    <PieChart width={400} height={400}>
      <Pie
        data={data}
        cx={200}
        cy={200}
        labelLine={false}
        label={entry => entry.name}
        outerRadius={80}
        fill="#8884d8"
        dataKey="value"
      >
        {data.map((entry, index) => (
          <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
        ))}
      </Pie>
      <Tooltip />
      <Legend />
    </PieChart>
  );
};

export default ProjectGraph;