import React, { Component } from 'react';
import { Card, CardHeader, CardBody, CardFooter, CardTitle } from 'reactstrap';

export default class MetadataDashboard extends Component {
  render() {
    return (
      <div className="animated fadeIn">
        <Card className="text-white bg-primary">
          <CardBody>
            <p>Metadata Dashboard.</p>
          </CardBody>
          <div style={{ height: '70px' }} />
        </Card>
      </div>
    );
  }
}
